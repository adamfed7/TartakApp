using Tartak.Magazyn.Models;
using Tartak.Sklep.Context;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Sklep.Helpers
{
    public class ProductHelper : IProductHelper
    {
        private readonly ShopContext _context;

        public ProductHelper(ShopContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductShopModel> GetAllProducts()
        {
            return _context.Products.AsEnumerable();
        }
        public ProductShopModel GetProductById(int id)
        {
            return _context.Products.Where(x => x.Id == id).Single();
        }
        public async Task EditProductAsync(ProductShopModel product)
        {
            var trackingProduct = GetProductById(product.Id);
            trackingProduct.Name = product.Name;
            trackingProduct.Description = product.Description;
            trackingProduct.PurchasePrice = product.PurchasePrice;
            //trackingProduct.QuantityInShop = product.QuantityInShop;
            _context.Products.Update(trackingProduct);
            await _context.SaveChangesAsync();
        }
        public async Task SendToWarehouseAsync(ProductShopModel product)
        {

        }
    }
}
