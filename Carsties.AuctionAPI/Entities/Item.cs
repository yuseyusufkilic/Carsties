﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Carsties.AuctionAPI.Entities
{
    [Table("Items")] //we didnt create dbset so we are specifying the table name here.
    public class Item
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public int? Mileage { get; set; }
        public string ImageUrl { get; set; }
        public Auction Auction { get; set; }
        public Guid AuctionId { get; set; }
    }
}
