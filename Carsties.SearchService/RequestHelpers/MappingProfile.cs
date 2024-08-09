using AutoMapper;
using Carsties.SearchService.Models;
using Contracts;

namespace Carsties.SearchService.RequestHelpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AuctionCreated, Item>();
            
        }
    }
}
