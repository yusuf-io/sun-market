using System;
using System.Collections.Generic;

namespace SunMarket.Data.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public Customer Customer { get; set; }
        public List<SalesOrderItem> salesOrderItems {get; set;}
        public bool IsPaid { get; set; }

    }
}
