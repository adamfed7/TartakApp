using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tartak.WebApp.Shared.Models
{
    public class ProductModel
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("purchasePrice")]
        public decimal PurchasePrice { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

    }
}
