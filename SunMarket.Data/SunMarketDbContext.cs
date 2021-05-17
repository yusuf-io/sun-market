using System;
using System.Text;
using SunMarket.Data.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SunMarket.Data
{
     public class SunMarketDbContext : IdentityDbContext
    {
        public SunMarketDbContext() { }
        public SunMarketDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}
