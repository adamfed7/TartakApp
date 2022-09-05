using Tartak.Magazyn.Models;

namespace Tartak.Sklep.Helpers
{
    public interface IProductHelper
    {
        Task AddProductAsync(ProductShopModel product);
        Task DeleteProductAsync(ProductShopModel product);
        IEnumerable<ProductShopModel> GetAllProducts();
        ProductShopModel GetProductById(int id);
        Task UpdateProductAsync(ProductShopModel product);
        Task BackToWareHouse(ProductShopModel product);
    }
}