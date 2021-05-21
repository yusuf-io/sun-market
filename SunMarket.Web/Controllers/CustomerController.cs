using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunMarket.Services.Customer;
using SunMarket.Web.Serialization;
using SunMarket.Web.ViewModels;
using System;
using System.Linq;

namespace SunMarket.Web.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController (ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("/api/customers")]
        public ActionResult GetCustomers()
        {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAllCustomers();
            var customerModels = customers.Select(customer => CustomerMapper.SerializesCustomer(customer))
                                          .OrderByDescending(customer => customer.CreateOn)
                                          .ToList();
            return Ok(customerModels);
        }

        [HttpDelete("/api/customers/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customerModel)
        {
            _logger.LogInformation("Creating new customer");
            customerModel.CreateOn = DateTime.UtcNow;
            customerModel.UpdateOn = DateTime.UtcNow;
            customerModel.PrimaryAddress.CreateOn = DateTime.UtcNow;
            customerModel.PrimaryAddress.UpdateOn = DateTime.UtcNow;

            var customer = CustomerMapper.SerializeCustomer(customerModel);
            var newCustomer = _customerService.CreateCustomer(customer);
            return Ok(newCustomer);
        }
    }
}
