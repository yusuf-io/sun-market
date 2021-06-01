using Microsoft.EntityFrameworkCore;
using SunMarket.Data;
using Xunit;
using SunMarket.Services.Customer;
using SunMarket.Data.Models;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace SunMarket.Test
{
    public class TestCustomerService
    {
        [Fact]
        public void CustomerService_GetAllCustomers_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<SunMarketDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new SunMarketDbContext(options);

             var service = new CustomerService(context);

            service.CreateCustomer(new Customer { Id = 123 });
            service.CreateCustomer(new Customer { Id = -213});

            var allCustomer = service.GetAllCustomers();

            allCustomer.Count.Should().Be(2);

        }

        [Fact]
        public void CustomerService_CreateCustomer_GivenNewCustomerObject()
        {
            var options = new DbContextOptionsBuilder<SunMarketDbContext>()
                .UseInMemoryDatabase("Add_writes_to_database").Options;

            using var context = new SunMarketDbContext(options);

            var service = new CustomerService(context);

            service.CreateCustomer(new Customer { Id = 1234 });

            context.Customers.Single().Id.Should().Be(1234);
        }

        [Fact]
        public void CustomerService_DeleteCustomer_GivenId()
        {
            var options = new DbContextOptionsBuilder<SunMarketDbContext>()
                .UseInMemoryDatabase("delete_one").Options;

            using var context = new SunMarketDbContext(options);

            var service = new CustomerService(context);

            service.CreateCustomer(new Customer { Id = 12345 });

            service.DeleteCustomer(12345);

            var allCustomer = service.GetAllCustomers();

            allCustomer.Count.Should().Be(0);
        }

        [Fact]
        public void CustomerService_OrderByLastName_WhenGetAllCustomersInvoked()
        {

            // Arrange

            var data = new List<Customer>
            {
                new Customer {Id = 123, LastName = "Zekeriya"},
                new Customer {Id = 12, LastName = "Ali"},
                new Customer {Id = -13, LastName = "Omar"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<SunMarketDbContext>();

            mockContext.Setup(c => c.Customers)
                .Returns(mockSet.Object);

            // Actions

            var sut = new CustomerService(mockContext.Object);
            var customers = sut.GetAllCustomers();

            // Assert

            customers.Count.Should().Be(3);
            customers[0].Id.Should().Be(12);
            customers[1].Id.Should().Be(-13);
            customers[2].Id.Should().Be(123);

        }
    }
}
