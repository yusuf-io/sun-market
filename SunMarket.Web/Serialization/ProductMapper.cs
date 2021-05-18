using SunMarket.Data.Models;
using SunMarket.Web.ViewModels;

namespace SunMarket.Web.Serialization
{
    public class ProductMapper
    {
        /// <summary>
        /// Maps a Product data model to a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreateOn = product.CreateOn,
                UpdateOn = product.UpdateOn,
                Name = product.Name,
                Description = product.Description,
                Price=product.Price,
                IsTaxable=product.IsTaxable,
                IsArchived =product.IsArchived
            };

        }

        /// <summary>
        /// Maps a ProductModel view model to a Product data model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Product SerializeProduct(ProductModel product)
        {
            return new Product
            {
                Id = product.Id,
                CreateOn = product.CreateOn,
                UpdateOn = product.UpdateOn,
                Name = product.Name,
                Description = product.Description,
                Price=product.Price,
                IsTaxable=product.IsTaxable,
                IsArchived =product.IsArchived
            };
        }
    }
}
