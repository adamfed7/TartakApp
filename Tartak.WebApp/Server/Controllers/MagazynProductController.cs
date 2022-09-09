using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Library.Data;

namespace Tartak.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagazynProductController : ControllerBase
    {
        private readonly IWarehouseProductData _productData;

        public MagazynProductController(IWarehouseProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IEnumerable<ProductWarehouseModel>> Get()
        {
            return await _productData.GetProductsAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ProductWarehouseModel> Get(int id)
        {
            return await _productData.GetProductByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task Post([FromBody] ProductWarehouseModel value)
        {
            await _productData.CreateProduct(value);
        }
        [HttpPost("SendToShop")]
        [Authorize(Roles = "Manager")]
        public async Task SendToShop([FromBody] ProductWarehouseModel value)
        {
            await _productData.SendToShop(value);
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task Put([FromBody] ProductWarehouseModel value)
        {
            await _productData.UpdateProduct(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task Delete(int id)
        {
            await _productData.DeleteProduct(id);
        }
    }
}
