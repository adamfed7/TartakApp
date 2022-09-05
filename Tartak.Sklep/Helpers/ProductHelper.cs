using Tartak.Magazyn.Models;
using Tartak.Sklep.Context;

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
            return _context.Products;
        }
        public ProductShopModel GetProductById(int id)
        {
            return _context.Products.Where(x => x.Id == id).Single();
        }
        public async Task AddProductAsync(ProductShopModel product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(ProductShopModel product)
        {
            var trackingProduct = GetProductById(product.Id);
            trackingProduct.Name = product.Name;
            trackingProduct.Description = product.Description;
            trackingProduct.PurchasePrice = product.PurchasePrice;
            trackingProduct.QuantityInShop = product.QuantityInShop;
            _context.Products.Update(trackingProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(ProductShopModel product)
        {
            _context.Products.Remove(GetProductById(product.Id));
            await _context.SaveChangesAsync();
        }
        public async Task BackToWareHouse(ProductShopModel product)
        {
            throw new NotImplementedException();
        }
    }
}
