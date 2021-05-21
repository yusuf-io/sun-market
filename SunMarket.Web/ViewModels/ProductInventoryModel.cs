using System;

namespace SunMarket.Web.ViewModels
{
    public class ProductInventoryModel
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public int QuantityOnHund { get; set; }
        public int IdealQuantity { get; set; }
        public ProductModel Product { get; set; }
    }
}
