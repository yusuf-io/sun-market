using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunMarket.Services.Inventory;
using SunMarket.Web.Serialization;
using SunMarket.Web.ViewModels;
using System;
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

            _logger.LogInformation($"Updating inventory for {shipment.ProductInventoryId} - Adjustment: {shipment.Adjustment}");
            var inventory = _inventoryService.UpdateUnitsAvailable(shipment.ProductInventoryId, shipment.Adjustment);
            return Ok(inventory);
        }

        [HttpGet("/api/inventory/snapshot")]
        public ActionResult GetSnapshotHistory()
        {

            _logger.LogInformation("Getting snapshot history");
            try
            {
                var snapshotHistory = _inventoryService.GetSnapshotHistory();

                // Get distinct points in a time a snapshot was collected
                var timelineMarker = snapshotHistory.Select(t => t.SnapshotTime).Distinct().ToList();

                // Get quantities grouped by id 
                var snapshots = snapshotHistory
                                .GroupBy(hist => hist.Product, hist => hist.QuantiyOnHand,
                                (key, group) => new ProductInventorySnapshotModel
                                {
                                    ProductId = key.Id,
                                    QuantityOnHand = group.ToList()
                                })
                                .OrderBy(hist => hist.ProductId)
                                .ToList();

                var viewModal = new SnapshotResponse
                {
                    Timeline = timelineMarker,
                    ProductInventorySnapshots = snapshots
                };
                return Ok(viewModal);
            }

            catch(Exception e)
            {
                _logger.LogError("Error getting snapshot history");
                _logger.LogError(e.StackTrace);
                return BadRequest("Error retrieving history");
            }
        }
    }
}
