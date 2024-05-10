using Carsties.SearchService.Data;
using Carsties.SearchService.Models;
using Carsties.SearchService.Services;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>(); //http service registeriation.


var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    await DbInitializer.InitDb(app);
}
catch (Exception e)
{

    Console.WriteLine(e);
}

app.Run();
