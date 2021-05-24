using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunMarket.Services.Product;
using SunMarket.Web.Serialization;
using SunMarket.Web.ViewModels;
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
        public ActionResult GetProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(product => ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels);
        }  
        
        [HttpPost("/api/products")]
        public ActionResult AddProduct([FromBody] ProductModel product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Adding new product");
            var productDataModel = ProductMapper.SerializeProduct(product);
            var result = _productService.CreateProduct(productDataModel);
            return Ok(result);
        }

        [HttpPatch("/api/products/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation("Arching a product");
            var archiveResult = _productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }
    }
}
