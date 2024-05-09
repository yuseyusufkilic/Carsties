using Carsties.SearchService.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace Carsties.SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController:ControllerBase
    {
        //we are using ActionResult here instead of the interface because we can specify the type of response like this ActionResult<Items>.
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm,string orderBy, string filterBy, string seller, string winner,int pageSize=4,int pageNumber=1)
        {
            var query = DB.PagedSearch<Item,Item>();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }
            query = orderBy switch
            {
                "make" => query.Sort(x => x.Ascending(w => w.Make)),
                "new" => query.Sort(s => s.Ascending(w => w.CreatedAt)),
                _ => query.Sort(l => l.Ascending(w => w.AuctionEnd))
            };
            query = filterBy switch
            {
                "finished" => query.Match(s => s.AuctionEnd < DateTime.Now),
                "endingSoon" => query.Match(i => i.AuctionEnd < DateTime.UtcNow.AddHours(6) && i.AuctionEnd > DateTime.UtcNow),
                _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
            };

            if (!string.IsNullOrEmpty(seller))
                query.Match(s => s.Seller == seller);
            if (!string.IsNullOrEmpty(winner))
                query.Match(s => s.Winner == winner);

            query.PageNumber(pageNumber);
            query.PageSize(pageSize);

            var result=await query.ExecuteAsync();

            return Ok(new {
                result.Results,
                result.TotalCount,
                result.PageCount
            });

        }

    }
}
