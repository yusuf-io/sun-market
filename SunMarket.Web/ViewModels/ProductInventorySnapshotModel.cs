using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunMarket.Web.ViewModels
{
    public class ProductInventorySnapshotModel
    {
        public int ProductId { get; set; }
        public List <int> QuantityOnHand { get; set; }
    }

    /// <summary>
    /// snapshot history in format suitable for graphing
    /// </summary>
    public class SnapshotResponse
    {
        public List <ProductInventorySnapshotModel> ProductInventorySnapshots { get; set; }
        public List <DateTime> Timeline { get; set; }
    }
}
