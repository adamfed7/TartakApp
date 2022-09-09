using Microsoft.AspNetCore.Mvc;
using Tartak.Magazyn.Helpers;
using Tartak.Magazyn.Models;

namespace Tartak.Magazyn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly IProductHelper _product;

        public ProductWarehouseController(IProductHelper product)
        {
            _product = product;
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
        public async Task Create([FromBody] ProductShopModel model)
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