using Tartak.Magazyn.Models;

namespace Tartak.Sklep.Helpers
{
    public interface IProductHelper
    {
        Task GetFromWarehouseAsync(ProductShopModel product);
        IEnumerable<ProductShopModel> GetAllProducts();
        ProductShopModel GetProductById(int id);
        Task EditProductAsync(ProductShopModel product);
        Task SendToWarehouseAsync(ProductShopModel product);
    }
}