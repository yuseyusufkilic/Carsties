using Carsties.AuctionAPI.Enums;
using System.Security.Cryptography;

namespace Carsties.AuctionAPI.Entities
{
    public class Auction
    {
        private Guid _id;
        public Guid Id
        {
            get
            {            
                if (_id == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }
            set
            {
               
                _id = value == Guid.Empty ? Guid.NewGuid() : value;
            }
        }    
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionEnd { get; set; }
        public Status Status { get; set; } = Status.Live;
        public Item Item { get; set; }
    }
}
