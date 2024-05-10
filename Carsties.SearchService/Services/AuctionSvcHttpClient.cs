using Carsties.SearchService.Models;
using MongoDB.Entities;

namespace Carsties.SearchService.Services
{
    public class AuctionSvcHttpClient
    {
        public HttpClient _httpClient { get; }
        public IConfiguration _config { get; }
        public AuctionSvcHttpClient(HttpClient httpClient,IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<Item>> GetItemsAsync()
        {
            var lastUpdatedDateTime = await DB.Find<Item,string>() // this one is giving us to latest auction record's date in mongodb. if it is not updated then we'll send a request with datetime.
                .Sort(x=>x.Descending(x=>x.UpdatedAt))
                .Project(x=>x.UpdatedAt.ToString())
                .ExecuteFirstAsync();

            if(string.IsNullOrEmpty(lastUpdatedDateTime))
                lastUpdatedDateTime = DateTime.MinValue.ToString();

            return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceURL"] + "/api/auctions?date=" + lastUpdatedDateTime);


        }

    }
}
