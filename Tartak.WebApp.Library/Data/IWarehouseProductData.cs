using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Library.Data
{
    public interface IWarehouseProductData
    {
        Task CreateProduct(ProductWarehouseModel product);
        Task DeleteProduct(int id);
        Task<ProductWarehouseModel> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductWarehouseModel>> GetProductsAsync();
        Task UpdateProduct(ProductWarehouseModel product);
        Task SendToShop(ProductShopModel product);
    }
}