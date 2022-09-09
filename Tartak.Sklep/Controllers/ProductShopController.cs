using Microsoft.AspNetCore.Mvc;
using Tartak.Magazyn.Models;
using Tartak.Sklep.Helpers;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Sklep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductShopController : ControllerBase
    {
        private readonly IProductHelper _product;

        public ProductShopController(IProductHelper product)
        {
            _product = product;
        }
        [HttpGet]
        public IEnumerable<ProductShopModel> Get()
        {
            return _product.GetAllProducts();
        }
        [HttpGet("{id}")]
        public ProductShopModel GetById(int id)
        {
            return _product.GetProductById(id);
        }
        [HttpPut]
        public async Task EditProduct([FromBody] ProductShopModel model)
        {
            await _product.EditProductAsync(model);
        }
        [HttpPost("SendToWarehouse/{id}")]
        public async Task SendToWarehouse(ProductShopModel model)
        {
            await _product.SendToWarehouseAsync(model);
        }
    }
}
