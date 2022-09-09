using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Library.Data
{
    public class WarehouseProductData : IWarehouseProductData
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WarehouseProductData(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IEnumerable<ProductWarehouseModel>> GetProductsAsync()
        {
            var url = _configuration["Urls:WarehouseBase"] + "ProductWarehouse";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await JsonSerializer.DeserializeAsync<IEnumerable<ProductWarehouseModel>>(await response.Content.ReadAsStreamAsync());
                return result;
            }
            throw new HttpRequestException();
        }
        public async Task<ProductWarehouseModel> GetProductByIdAsync(int id)
        {
            var url = _configuration["Urls:WarehouseBase"] + $"ProductWarehouse/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await JsonSerializer.DeserializeAsync<ProductWarehouseModel>(await response.Content.ReadAsStreamAsync());
                return result;
            }
            throw new HttpRequestException();
        }
        public async Task CreateProduct(ProductWarehouseModel product)
        {
            var url = _configuration["Urls:WarehouseBase"] + $"ProductWarehouse";
            string json = JsonSerializer.Serialize(product);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException();
        }
        public async Task DeleteProduct(int id)
        {
            var url = _configuration["Urls:WarehouseBase"] + $"ProductWarehouse/{id}";
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException();
        }
        public async Task UpdateProduct(ProductWarehouseModel product)
        {
            var url = _configuration["Urls:WarehouseBase"] + $"ProductWarehouse";
            string json = JsonSerializer.Serialize(product);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException();
        }
        public async Task SendToShop(ProductWarehouseModel product)
        {
            var model = new ProductShopModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = 0,
                PurchasePrice = product.PurchasePrice,
                QuantityInShop = (uint)product.QuantityInWarehouse
            };
            var url = _configuration["Urls:WarehouseBase"] + $"ProductWarehouse/SendToShop";
            string json = JsonSerializer.Serialize(model);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            
            throw new HttpRequestException(response.StatusCode.ToString());
        }
    }
}
