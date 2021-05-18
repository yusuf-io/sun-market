using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunMarket.Services.Product;
using SunMarket.Web.Serialization;
using System.Linq;

namespace SunMarket.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet("/api/products")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Get all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(product => ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels);
        }
    }
}
