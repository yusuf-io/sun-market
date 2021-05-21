using SunMarket.Data.Models;
using SunMarket.Web.ViewModels;

namespace SunMarket.Web.Serialization
{
    public class InventoryMapper
    {
        /// <summary>
        /// Serializes a ProductInventory data model to ProductInventoryModel view model
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public static ProductInventoryModel SerializesProductInventory(ProductInventory inventory)
        {
            return new ProductInventoryModel
            {
                Id = inventory.Id,
                CreateOn = inventory.CreateOn,
                UpdateOn = inventory.UpdateOn,
                QuantityOnHund = inventory.QuantityOnHund,
                IdealQuantity = inventory.IdealQuantity,
                Product = ProductMapper.SerializeProductModel(inventory.Product)
            };
        }

        /// <summary>
        /// Serializes a ProductInventoryModel view model to ProductInventory data model
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public static ProductInventory SerializesProductInventoryModel(ProductInventoryModel inventory)
        {
            return new ProductInventory
            {
                Id = inventory.Id,
                CreateOn = inventory.CreateOn,
                UpdateOn = inventory.UpdateOn,
                QuantityOnHund = inventory.QuantityOnHund,
                IdealQuantity = inventory.IdealQuantity,
                Product = ProductMapper.SerializeProduct(inventory.Product)
            };
        }
    }
}
