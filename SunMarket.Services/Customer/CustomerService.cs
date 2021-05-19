using Microsoft.EntityFrameworkCore;
using SunMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SunMarket.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SunMarketDbContext _db;
        public CustomerService (SunMarketDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Add a new customer to the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        ServiceResponse<Data.Models.Customer> ICustomerService.CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Message = "Saved new customer",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }

            catch(Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
            
        }

        /// <summary>
        /// Deletes a cutomer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResponse<bool> ICustomerService.DeleteCustomer(int id)
        {
           
             var customer = _db.Customers.Find(id);
                
                if (customer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Customer to delete not found!",
                        Time = DateTime.UtcNow,
                        Data = false
                    };
                }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Customer deleted!",
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
        /// Retrieves all customers from the database
        /// </summary>
        /// <returns></returns>
        List<Data.Models.Customer> ICustomerService.GetAllCustomers()
        {
            return _db.Customers.Include(customer=>customer.PrimaryAddress)
                                .OrderBy(customer=>customer.LastName)
                                .ToList();
        }

        /// <summary>
        /// Retrieves a customer by Id from the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Data.Models.Customer ICustomerService.GetCustomerById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}
