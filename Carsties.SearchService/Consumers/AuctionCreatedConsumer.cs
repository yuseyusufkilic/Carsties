using AutoMapper;
using Carsties.SearchService.Models;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace Carsties.SearchService.Consumers
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        private readonly IMapper _mapper;

        public AuctionCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine($"Consimung auction created:{context.Message}");
            var item = _mapper.Map<Item>(context.Message);
            await item.SaveAsync();

        }
    }
}
