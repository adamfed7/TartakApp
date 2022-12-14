
using Tartak.Magazyn.Context;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public class ProductHelper : IProductHelper
    {
        private readonly WarehouseContext _context;
        private readonly IProductSenderHelper _sender;

        public ProductHelper(WarehouseContext context, IProductSenderHelper sender)
        {
            _context = context;
            _sender = sender;
        }
        public IEnumerable<ProductWarehouseModel> GetAllProducts()
        {
            return _context.Products.Where(x => x.IsActual == true && x.QuantityInWarehouse > 0).AsEnumerable();
        }
        public ProductWarehouseModel GetProductById(int id)
        {
            return _context.Products.Where(x => x.IsActual == true && x.Id == id && x.QuantityInWarehouse > 0).Single();
        }
        public async Task AddProductAsync(ProductWarehouseModel product)
        {
            product.IsActual = true;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task SendToShop(ProductShopModel productShopModel)
        {
            var product = GetAllProducts().Where(x => x.Id == productShopModel.Id).FirstOrDefault();
            if (product?.QuantityInWarehouse >= productShopModel.QuantityInShop)
            {
                product.QuantityInWarehouse -= productShopModel.QuantityInShop;
                await UpdateProductAsync(product);
                productShopModel.Name = product.Name;
                productShopModel.Description = product.Description;
                productShopModel.PurchasePrice = product.PurchasePrice;

                await _sender.Send(productShopModel);
            }
        }
        public async Task UpdateProductAsync(ProductWarehouseModel product)
        {
            var trackingProduct = GetProductById(product.Id);
            trackingProduct.Name = product.Name;
            trackingProduct.Description = product.Description;
            trackingProduct.PurchasePrice = product.PurchasePrice;
            trackingProduct.QuantityInWarehouse = product.QuantityInWarehouse;
            _context.Products.Update(trackingProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            _context.Products.Where(x => x.Id == id).Single().IsActual = false;
            await _context.SaveChangesAsync();
        }
    }
}
