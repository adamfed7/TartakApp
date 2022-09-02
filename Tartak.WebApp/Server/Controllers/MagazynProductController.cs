using Microsoft.AspNetCore.Mvc;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MagazynProductController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            var data = new List<ProductModel>();
            data.Add(new ProductModel()
            {
                Id = 0,
                Name = "Product",
                Description = "The best product",
                Price = 123.123m,
                Quantity = 22
            });
            return data.AsEnumerable();
        }

        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            return new ProductModel()
            {
                Id = 0,
                Name = "Product",
                Description = "The best product",
                Price = 123.123m,
                Quantity = 22
            };
        }

        [HttpPost]
        public void Post([FromBody] ProductModel value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductModel value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
