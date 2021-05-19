using System.Collections.Generic;

namespace SunMarket.Services.Inventory
{
    public interface IInventoryService
    {
        public List<Data.Models.ProductInventory> GetCurrentInventory();
        public Data.Models.ProductInventory GetProductInventoryByProductId(int id);
        public ServiceResponse<Data.Models.ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public List<Data.Models.ProductInventorySnapshot> GetSnapshotHistory();
    }
}
