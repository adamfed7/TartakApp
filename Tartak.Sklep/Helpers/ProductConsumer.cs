using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tartak.Magazyn.Models;
using Tartak.Sklep.Context;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Sklep.Helpers
{
    public class ProductConsumer : IProductConsumer
    {
        private readonly ShopContext _context;
        private readonly IProductHelper _productHelper;

        public ProductConsumer(ShopContext context,IProductHelper productHelper)
        {
            _context = context;
            _productHelper = productHelper;
        }
        public async Task Consume(ConsumeContext<ProductShopModel> context)
        {
            var reqProduct = context.Message;
            var product = _productHelper.GetAllProducts().Where(x => x.Id == reqProduct.Id).FirstOrDefault();
            if(product == null)
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _context.Products.AddAsync(reqProduct);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
            }
            else
            {
                product.QuantityInShop += reqProduct.QuantityInShop;
                product.Price = reqProduct.Price;
                await _productHelper.EditProductAsync(product);
            }
        }
    }
}
