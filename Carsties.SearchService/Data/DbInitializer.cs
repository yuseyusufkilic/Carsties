using Carsties.SearchService.Models;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Text.Json;

namespace Carsties.SearchService.Data
{
    public class DbInitializer
    {
        public async static Task InitDb(WebApplication app)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings
                    .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();
            if (count ==0)
            {
                Console.WriteLine("There is no data -  will do the seed data insertion.");
                var itemsFromJson = await File.ReadAllTextAsync("Data/auctions.json");
                
                var serializeOpt = new JsonSerializerOptions { PropertyNameCaseInsensitive=true };
                var itemsSerialized = JsonSerializer.Deserialize<List<Item>>(itemsFromJson, serializeOpt);

                await DB.SaveAsync(itemsSerialized); //creates new entites or replace if they exists when we pass parametere here.

            }
        }
    }
}
