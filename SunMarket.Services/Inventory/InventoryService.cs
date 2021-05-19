using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SunMarket.Data;
using SunMarket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SunMarket.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SunMarketDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(SunMarketDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(productInventory=>productInventory.Product)
                .Where(productInventory => !productInventory.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Retrieves a ProductInventory by product id from the database
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetProductInventoryByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(productInventory => productInventory.Product)
                .FirstOrDefault(productInventory => productInventory.Product.Id == productId);
        }

        /// <summary>
        /// Retrieves Snapshot history for the previous 24 hours
        /// </summary>
        /// <returns></returns>
        public List <ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(24);

            return _db.ProductInventorySnapshots
                .Include(snapshot => snapshot.Product)
                .Where(snapshot => !snapshot.Product.IsArchived && snapshot.SnapshotTime > earliest)
                .ToList();
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjusts QuantityOnHund by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added / removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(productInventory=>productInventory.Product)
                    .First(productInventory=>productInventory.Product.Id==id);

                inventory.QuantityOnHund += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }

                catch(Exception e)
                {
                    _logger.LogError("Error creating inventory snapshot.");
                    _logger.LogError(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Message = $"Product {id} inventory adjusted by {adjustment}",
                    Time = DateTime.UtcNow,
                    Data = inventory
                };
            }

            catch(Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
             
            }
        }

        /// <summary>
        /// Creates a snapshot record using the provided ProductInventory instance
        /// </summary>
        /// <param name="inventory"></param>
          private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = DateTime.UtcNow,
                QuantiyOnHand = inventory.QuantityOnHund,
                Product = inventory.Product
            };

            _db.ProductInventorySnapshots.Add(snapshot);
        }
    }
}
