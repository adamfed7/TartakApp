using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Library.Data
{
    public interface IShopProductData
    {
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task UpdateProduct(ProductModel product);
    }
}