using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public void GetFromWarehouse([FromBody] ProductModel value)
        {

        }

        [HttpPut("EditProduct/{id}")]
        public void EditProduct(int id, [FromBody] ProductModel value)
        {
        }

        [HttpPost("SendToWarehouse/{id}")]
        public void SendToWarehouse(int id)
        {
        }
    }
}
