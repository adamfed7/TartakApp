using Microsoft.AspNetCore.Mvc;
using Tartak.WebApp.Library.Data;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SklepProductController : ControllerBase
    {
        private readonly ShopProductData _productData;

        public SklepProductController(ShopProductData productData)
        {
            _productData = productData;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await _productData.GetProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductModel> Get(int id)
        {
            return await _productData.GetProductByIdAsync(id);
        }

        [HttpPost("GetFromWarehouse")]
        public async Task GetFromWarehouse([FromBody] ProductModel value)
        {

        }

        [HttpPut]
        public async Task EditProduct([FromBody] ProductModel value)
        {
            await _productData.UpdateProduct(value);
        }

        [HttpPost("SendToWarehouse/{id}")]
        public async Task SendToWarehouse(int id)
        {
        }
    }
}
