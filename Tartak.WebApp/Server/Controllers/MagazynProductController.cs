using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagazynProductController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
        public void Post([FromBody] ProductModel value)
        {
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public void Put(int id, [FromBody] ProductModel value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public void Delete(int id)
        {
        }
    }
}
