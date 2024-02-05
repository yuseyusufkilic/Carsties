using Carsties.AuctionAPI.DTOs;
using Carsties.AuctionAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carsties.AuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
        {
            return await _auctionService.GetAllAuctions();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
            var auction = await _auctionService.GetAuctionById(id);
            if (auction is null) return NoContent();
                        
            return Ok(auction);
        }
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto createAuctionDto)
        {
            var createResponse=await _auctionService.CreateAuction(createAuctionDto);
            if (createResponse is not null)
            {
                return CreatedAtAction(nameof(GetAuctionById), new {createResponse.Id },createResponse);

            }
            return BadRequest("Cannot perform create operation");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
        {
            var updateResponse=await _auctionService.UpdateAuction(id,updateAuctionDto) > 0;
            if (updateResponse) return Ok($"Auction is updated. | SaveChangesAsync returns : {updateResponse}");

            return BadRequest("Update process cannot performed succesfully");
                      
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var result = await _auctionService.DeleteAuction(id) > 0;
            if (result) return Ok("DeleteAuction DB changes performed successfully");

            return BadRequest("Db changes cannot performed succesfully");
                                     
        }
    }
}
