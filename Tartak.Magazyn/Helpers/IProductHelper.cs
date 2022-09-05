using Tartak.Magazyn.Models;

namespace Tartak.Magazyn.Helpers
{
    public interface IProductHelper
    {
        Task AddProductAsync(ProductWarehouseModel product);
        Task DeleteProductAsync(ProductWarehouseModel product);
        IEnumerable<ProductWarehouseModel> GetAllProducts();
        ProductWarehouseModel GetProductById(int id);
        Task UpdateProductAsync(ProductWarehouseModel product);
    }
}