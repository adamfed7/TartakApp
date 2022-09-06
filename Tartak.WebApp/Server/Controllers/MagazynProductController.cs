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
        private readonly WarehouseProductData _productData;

        public MagazynProductController(WarehouseProductData productData)
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

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async void Put([FromBody] ProductWarehouseModel value)
        {
            await _productData.UpdateProduct(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async void Delete(int id)
        {
            await _productData.DeleteProduct(id);
        }
    }
}
