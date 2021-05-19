using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SunMarket.Data;
using SunMarket.Data.Models;
using SunMarket.Services.Inventory;
using SunMarket.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SunMarket.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SunMarketDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        public OrderService(SunMarketDbContext dbContext, ILogger<OrderService> logger, IProductService productService, IInventoryService inventoryService)
        {
            _db = dbContext;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
         ServiceResponse<bool> IOrderService.GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating open order");

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);

                var inventory = _inventoryService.GetProductInventoryByProductId(item.Product.Id);
                _inventoryService.UpdateUnitsAvailable(inventory.Id, -item.Quantity);
            }
                try
                {
                    _db.SalesOrders.Add(order);
                    _db.SaveChanges();

                    return new ServiceResponse<bool>
                    {
                        IsSuccess = true,
                        Message = "Open order created",
                        Time = DateTime.UtcNow,
                        Data = true
                    };
                }

                catch(Exception e)
                {
                    return new ServiceResponse<bool>
                    {
                        IsSuccess = false,
                        Message = e.StackTrace,
                        Time = DateTime.UtcNow,
                        Data = false
                    };
                }
        }

        /// <summary>
        /// Retrieves all SalesOrders from the database
        /// </summary>
        /// <returns></returns>
        List<SalesOrder> IOrderService.GetAllOrders()
        {
            return _db.SalesOrders
                .Include(order => order.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .Include(order => order.SalesOrderItems)
                    .ThenInclude( salesOrderItem => salesOrderItem.Product)
                .ToList();
        }

        /// <summary>
        /// Marks an open SalesOrder as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResponse<bool> IOrderService.MarkFulfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            order.UpdateOn = DateTime.UtcNow;
            order.IsPaid = true;

            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = $"Open order {id} paid",
                    Time = DateTime.UtcNow,
                    Data = true
                };
            }

            catch(Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = false
                };
            }
        }
    }
}
