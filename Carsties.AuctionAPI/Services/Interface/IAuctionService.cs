using Carsties.AuctionAPI.DTOs;

namespace Carsties.AuctionAPI.Services.Interface
{
    public interface IAuctionService
    {
        Task<AuctionDto> CreateAuction(CreateAuctionDto createAuctionDto);
        Task<List<AuctionDto>> GetAllAuctions();
        Task<AuctionDto> GetAuctionById(Guid id);
        Task<int> UpdateAuction(Guid id ,UpdateAuctionDto updateAuctionDto);
        Task<int> DeleteAuction(Guid id);
    }
}
