using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Library.Data;
using Tartak.WebApp.Shared.Models;

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
        public void Post([FromBody] ProductWarehouseModel value)
        {
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public void Put(int id, [FromBody] ProductWarehouseModel value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public void Delete(int id)
        {
        }
    }
}
