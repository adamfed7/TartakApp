using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public interface IProductHelper
    {
        Task AddProductAsync(ProductWarehouseModel product);
        Task DeleteProductAsync(int id);
        IEnumerable<ProductWarehouseModel> GetAllProducts();
        ProductWarehouseModel GetProductById(int id);
        Task UpdateProductAsync(ProductWarehouseModel product);
        Task SendToShop(ProductShopModel productShopModel);
    }
}