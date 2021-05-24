using SunMarket.Data;
using SunMarket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SunMarket.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SunMarketDbContext _db;
        public ProductService(SunMarketDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Archives a product by setting boolean IsArchived to true
        /// </summary>        
        /// <param name="id">Filters</param>
        /// <returns></returns>
        ServiceResponse<Data.Models.Product> IProductService.ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = true,
                    Message = "Archived product",
                    Time = DateTime.UtcNow,
                    Data = product
                };
            }

            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Add a new product to the database
        /// </summary>        
        /// <param name="product">Filters</param>
        /// <returns></returns>
        ServiceResponse<Data.Models.Product> IProductService.CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = true,
                    Message = "Saved new product",
                    Time = DateTime.UtcNow,
                    Data = product
                };
            }

            catch(Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = product
                };
            }
        
        }

        /// <summary>
        /// Retrieves all products from the database
        /// </summary>        
        /// <returns></returns>
        List<Data.Models.Product> IProductService.GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves a product by Id from the database
        /// </summary>        
        /// <param name="id">Filters</param>
        /// <returns></returns>
        Data.Models.Product IProductService.GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
