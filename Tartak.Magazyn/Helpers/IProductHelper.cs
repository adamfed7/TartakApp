using Tartak.Magazyn.Models;

namespace Tartak.Magazyn.Helpers
{
    public interface IProductHelper
    {
        Task AddProductAsync(ProductWarehouseModel product);
        Task DeleteProduct(ProductWarehouseModel product);
        IEnumerable<ProductWarehouseModel> GetAllProducts();
        ProductWarehouseModel GetProductById(int id);
        Task UpdateProductAsync(ProductWarehouseModel product);
    }
}