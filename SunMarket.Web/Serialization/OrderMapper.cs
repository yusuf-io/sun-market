using SunMarket.Data.Models;
using SunMarket.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SunMarket.Web.Serialization
{
    public class OrderMapper
    {

        /// <summary>
        /// Maps an InvoiceModel view model to SalesOrder data model 
        /// </summary>
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.SalesOrderItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProduct(item.Product)
                }).ToList();

            return new SalesOrder
            {
                CreateOn = DateTime.Now,
                UpdateOn = DateTime.Now,
                SalesOrderItems = salesOrderItems
            };
        }

        /// <summary>
        /// Maps a collection SalesOrders data model to OrderModels view model
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModel(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                CreateOn = order.CreateOn,
                UpdateOn = order.UpdateOn,
                Customer = CustomerMapper.SerializesCustomer(order.Customer),
                SalesOrderItems = SerializesSalesOrderItems(order.SalesOrderItems),
                IsPaid = order.IsPaid
            }).ToList();
        }

        /// <summary>
        /// Maps a collection of SalesOrderItems data model to SalesOrderItemModels view model
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializesSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)

            }).ToList();
        }
    }
}
