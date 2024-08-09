using Carsties.AuctionAPI.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Carsties.AuctionAPI.Data
{
    public class AuctionDbContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; } // no need to create another dbset for items as they're related with auction ef will create automatically.
        public AuctionDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //inbox-outbox tabloları oluşturuyor mesajları manage edebilmek icin.
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
