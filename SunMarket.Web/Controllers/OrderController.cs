using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunMarket.Services.Customer;
using SunMarket.Services.Order;
using SunMarket.Web.Serialization;
using SunMarket.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunMarket.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Generating invoice");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetCustomerById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }

        [HttpGet("/api/orders")]
        public ActionResult GetOrders()
        {
            _logger.LogInformation("Getting orders");
            var orders = _orderService.GetAllOrders();
            var orderModels = OrderMapper.SerializeOrdersToViewModel(orders);
            return Ok(orderModels);
        }
        
        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            _logger.LogInformation($"Marking order {id} complete");
            _orderService.MarkFulfilled(id);
            return Ok();
        }
    }
}
