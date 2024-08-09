using AutoMapper;
using Carsties.AuctionAPI.DTOs;
using Carsties.AuctionAPI.Entities;
using Carsties.AuctionAPI.Services.Interface;
using Contracts;
using MassTransit;

namespace Carsties.AuctionAPI.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publish;

        public AuctionService(IAuctionRepository auctionRepo, IMapper mapper,IPublishEndpoint publish)
        {
            _auctionRepo = auctionRepo;
            _mapper = mapper;
            _publish = publish;
        }

        public async Task<AuctionDto> CreateAuction(CreateAuctionDto createAuctionDto)
        {
            var auction = _mapper.Map<Auction>(createAuctionDto);
            var newAuction = _mapper.Map<AuctionDto>(auction);
            await _auctionRepo.Create(auction);

            await _publish.Publish(_mapper.Map<AuctionCreated>(newAuction)); //publish satırını save changesten önceye aldık cunku artık outbox pattern var. fail olmasını sallamıyoruz. transaction yapısı var.
            int isAdded = await _auctionRepo.SaveChangesAsync();
            Console.WriteLine($"Entity state is {isAdded} ||| 1: added , 0: not added");
            if (isAdded > 0) return newAuction;

            return null;
        }

        public Task<int> DeleteAuction(Guid id)
        {
            return _auctionRepo.Delete(id);
        }

        public async Task<List<AuctionDto>> GetAllAuctions(string date)
        {
            var auctions = await _auctionRepo.GetAll();
            var auctionsUpdated=auctions.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            return _mapper.Map<List<AuctionDto>>(auctionsUpdated);
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
        
    


