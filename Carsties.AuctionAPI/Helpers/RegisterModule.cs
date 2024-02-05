using Autofac;
using Carsties.AuctionAPI.Services;
using Carsties.AuctionAPI.Services.Interface;
using System.Reflection;
using Module = Autofac.Module;

namespace Carsties.AuctionAPI.Helpers
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuctionRepository>().As<IAuctionRepository>();
            builder.RegisterType<AuctionService>().As<IAuctionService>();
        }

    }
}
