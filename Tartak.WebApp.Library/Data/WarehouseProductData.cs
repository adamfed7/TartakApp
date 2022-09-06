using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Tartak.Magazyn.Models;

namespace Tartak.WebApp.Library.Data
{
    public class WarehouseProductData
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
    }
}
