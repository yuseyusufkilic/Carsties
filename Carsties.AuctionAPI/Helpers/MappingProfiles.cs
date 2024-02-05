using AutoMapper;
using Carsties.AuctionAPI.DTOs;
using Carsties.AuctionAPI.Entities;

namespace Carsties.AuctionAPI.Helpers
{
    public class MappingProfiles:Profile
    {
        //takes reference as a property names. name matching.
        public MappingProfiles()
        {
            CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item); // include members diyerek , auctiondaki itemsa da bak diyor.
            CreateMap<Item, AuctionDto>();

            CreateMap<CreateAuctionDto, Auction>().ForMember(s => s.Item, w => w.MapFrom(ş=>ş)); // formember diyerek ise, dtodan auctionstaki item'a da bi şey gelior diyor.
            CreateMap<CreateAuctionDto, Item>();
            
            CreateMap<UpdateAuctionDto,Auction>().ForMember(s => s.Item, w => w.MapFrom(ş => ş));
            CreateMap<UpdateAuctionDto, Item>();

        }
    }
}
