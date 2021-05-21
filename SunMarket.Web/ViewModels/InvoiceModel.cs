using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunMarket.Web.ViewModels
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemModel> SalesOrderItems { get; set; }
    }
}
