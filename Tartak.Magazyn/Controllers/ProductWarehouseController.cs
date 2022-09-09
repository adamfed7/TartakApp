using Microsoft.AspNetCore.Mvc;
using Tartak.Magazyn.Helpers;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly IProductHelper _product;
        private readonly ILogger<ProductWarehouseController> _logger;

        public ProductWarehouseController(IProductHelper product,ILogger<ProductWarehouseController> logger)
        {
            _product = product;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<ProductWarehouseModel> Get()
        {
            return _product.GetAllProducts();
        }
        [HttpGet("{id}")]
        public ProductWarehouseModel GetById(int id)
        {
            return _product.GetProductById(id);
        }
        [HttpPost]
        public async Task Create([FromBody] ProductWarehouseModel model)
        {
            await _product.AddProductAsync(model);
        }
        [HttpPost("SendToShop")]
        public async Task SendToShop([FromBody] ProductShopModel model)
        {
            await _product.SendToShop(model);
        }
        [HttpPut]
        public async Task Update([FromBody] ProductWarehouseModel model)
        {
            await _product.UpdateProductAsync(model);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _product.DeleteProductAsync(id);
        }
    }
}