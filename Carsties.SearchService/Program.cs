using Carsties.SearchService.Consumers;
using Carsties.SearchService.Data;
using Carsties.SearchService.Models;
using Carsties.SearchService.Services;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>(); //http service registeriation.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search",false));
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.ConfigureEndpoints(ctx);
    });
}
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
