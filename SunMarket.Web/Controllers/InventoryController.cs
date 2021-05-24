using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunMarket.Services.Inventory;
using SunMarket.Web.Serialization;
using SunMarket.Web.ViewModels;
using System.Linq;

namespace SunMarket.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;
        public InventoryController (ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
              _logger = logger;
              _inventoryService = inventoryService;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventory");
            var inventories = _inventoryService.GetCurrentInventory();
            var inventoryModels = inventories.Select(inv => InventoryMapper.SerializesProductInventory(inv))
                                                .OrderBy(inv => inv.Product.Name)
                                                .ToList();
            return Ok(inventoryModels);
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation($"Updating inventory for {shipment.ProductId} - Adjustment: {shipment.Adjustment}");
            var inventory = _inventoryService.UpdateUnitsAvailable(shipment.ProductId, shipment.Adjustment);
            return Ok(inventory);
        }
    }
}
