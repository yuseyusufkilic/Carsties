using Carsties.AuctionAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carsties.AuctionAPI.Data
{
    public class AuctionDbContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; } // no need to create another dbset for items as they're related with auction ef will create automatically.
        public AuctionDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
