﻿using System;
using System.Text;
using System.Collections.Generic;

namespace SunMarket.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerAddress PrimaryAddress { get; set; }
    }
}
