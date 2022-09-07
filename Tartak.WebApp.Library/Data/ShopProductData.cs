using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Library.Data
{
    public class ShopProductData
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ShopProductData(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var url = _configuration["Urls:ShopBase"] + "ProductShop";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await JsonSerializer.DeserializeAsync<IEnumerable<ProductModel>>(await response.Content.ReadAsStreamAsync());
                return result;
            }
            throw new HttpRequestException();
        }
        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var url = _configuration["Urls:ShopBase"] + $"ProductShop/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await JsonSerializer.DeserializeAsync<ProductModel>(await response.Content.ReadAsStreamAsync());
                return result;
            }
            throw new HttpRequestException();
        }
        public async Task UpdateProduct(ProductModel product)
        {
            var url = _configuration["Urls:ShopBase"] + $"ProductShop";
            string json = JsonSerializer.Serialize(product);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new HttpRequestException();
        }
    }
}
