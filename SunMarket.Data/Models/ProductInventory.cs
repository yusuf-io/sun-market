using System;

namespace SunMarket.Data.Models
{
    public class ProductInventory
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public int QuantityOnHund { get; set; }
        public int IdealQuantity { get; set; }
        public Product Product { get; set; }
    }
}
