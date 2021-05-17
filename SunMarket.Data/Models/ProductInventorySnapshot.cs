using System;

namespace SunMarket.Data.Models
{
    public class ProductInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapshotTime{ get; set; } 
        public int QuantiyOnHand { get; set; }
        public Product Product { get; set; }


    }
}
