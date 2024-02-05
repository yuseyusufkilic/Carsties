using AutoMapper;
using Carsties.AuctionAPI.DTOs;
using Carsties.AuctionAPI.Entities;
using Carsties.AuctionAPI.Services.Interface;

namespace Carsties.AuctionAPI.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepo;
        private readonly IMapper _mapper;

        public AuctionService(IAuctionRepository auctionRepo, IMapper mapper)
        {
            _auctionRepo = auctionRepo;
            _mapper = mapper;
        }

        public async Task<AuctionDto> CreateAuction(CreateAuctionDto createAuctionDto)
        {
            var auction = _mapper.Map<Auction>(createAuctionDto);
            int isAdded = await _auctionRepo.Create(auction);
            if (isAdded > 0)
                return _mapper.Map<AuctionDto>(auction);
            return null;
        }

        public Task<int> DeleteAuction(Guid id)
        {
            return _auctionRepo.Delete(id);
        }

        public async Task<List<AuctionDto>> GetAllAuctions()
        {
            var auctions = await _auctionRepo.GetAll();
            return _mapper.Map<List<AuctionDto>>(auctions);
        }

        public async Task<AuctionDto> GetAuctionById(Guid id)
        {
            var auction = await _auctionRepo.GetById(id);
            return _mapper.Map<AuctionDto>(auction);
        }

        public async Task<int> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
        {
            var updateAuctionMapped = _mapper.Map<Auction>(updateAuctionDto);
            if (!(updateAuctionDto.Year.HasValue && updateAuctionDto.Mileage.HasValue))
            {

                updateAuctionMapped.Item.Year = null;
                updateAuctionMapped.Item.Mileage = null;
            }
            return await _auctionRepo.Update(id, updateAuctionMapped);
        }
    }
}   
        
    


