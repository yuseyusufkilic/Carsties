using Autofac;
using Autofac.Extensions.DependencyInjection;
using Carsties.AuctionAPI.Data;
using Carsties.AuctionAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new RegisterModule()));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("NpgsqlConnectionString"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //assembly içerisinde profile'dan miras alan bi class var mý bakýyor.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();
try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{

    Console.WriteLine(ex);
}
app.Run();
