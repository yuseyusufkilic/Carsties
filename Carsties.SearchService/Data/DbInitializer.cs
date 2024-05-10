using Carsties.SearchService.Models;
using Carsties.SearchService.Services;
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

            using var scope = app.Services.CreateScope();
            var httpClientSvc = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();
            var items = await httpClientSvc.GetItemsAsync();

            Console.WriteLine(items.Count + "returned from auction service.");
            if (items.Count > 0)
                await DB.SaveAsync(items);
            
           
        }
        }
    }

