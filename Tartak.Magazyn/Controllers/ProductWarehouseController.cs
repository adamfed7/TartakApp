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
        public async void Create([FromBody] ProductWarehouseModel model)
        {
            await _product.AddProductAsync(model);
        }
        [HttpPut]
        public async void Update([FromBody] ProductWarehouseModel model)
        {
            await _product.UpdateProductAsync(model);
        }
        [HttpDelete]
        public async void Delete(ProductWarehouseModel model)
        {
            await _product.DeleteProductAsync(model);
        }
    }
}
