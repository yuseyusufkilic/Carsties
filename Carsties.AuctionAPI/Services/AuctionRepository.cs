using Carsties.AuctionAPI.Data;
using Carsties.AuctionAPI.Entities;
using Carsties.AuctionAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Carsties.AuctionAPI.Services
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionDbContext _dbContext;

        public AuctionRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        public async Task<int> Create(Auction auction)
        {
            var entity = await _dbContext.Auctions.AddAsync(auction);
            var isStateAdded=entity.State == EntityState.Added;
            return isStateAdded ? 1 : 0;
        }

        public async Task<int> Delete(Guid id)
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == id);
            if (auction is not null)
            {
                _dbContext.Auctions.Remove(auction);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Auction>> GetAll()
        {
            var allAuctions = await _dbContext.Auctions
                .Include(x=>x.Item)
                .AsNoTracking()
                .OrderBy(w=>w.Item.Make)
                .ToListAsync();
            return allAuctions;
        }

        public async Task<Auction> GetById(Guid id)
        {
            var auction = await _dbContext.Auctions
                .Include(x=>x.Item)
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (auction is null)
                Console.WriteLine("Auction cannot found."); //TODO: log eklenecek.
            return auction;                     
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Guid id, Auction updateAuction)
        {
            var auction = await _dbContext.Auctions.Include(x=>x.Item).FirstOrDefaultAsync(x=>x.Id == id);
            if (auction is null) return 0;

            auction.Item.Make = updateAuction.Item.Make ?? auction.Item.Make;
            auction.Item.Model = updateAuction.Item.Model ?? auction.Item.Model;
            auction.Item.Color = updateAuction.Item.Color ?? auction.Item.Color;
            auction.Item.Mileage = updateAuction.Item.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = updateAuction.Item.Year ?? auction.Item.Year;

            return await _dbContext.SaveChangesAsync();

        }
    }
}
