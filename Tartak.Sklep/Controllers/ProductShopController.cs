using Microsoft.AspNetCore.Mvc;
using Tartak.Magazyn.Models;
using Tartak.Sklep.Helpers;

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
        [HttpPost]
        public async void Create([FromBody] ProductShopModel model)
        {
            await _product.AddProductAsync(model);
        }
        [HttpPut]
        public async void Update([FromBody] ProductShopModel model)
        {
            await _product.UpdateProductAsync(model);
        }
        [HttpDelete]
        public async void Delete(ProductShopModel model)
        {
            await _product.DeleteProductAsync(model);
        }
    }
}
