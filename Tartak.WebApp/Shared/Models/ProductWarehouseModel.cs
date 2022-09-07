using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tartak.Magazyn.Models
{
    public class ProductWarehouseModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("purchasePrice")]
        public decimal PurchasePrice { get; set; }
        [JsonPropertyName("quantityInWarehouse")]
        public int QuantityInWarehouse { get; set; }

    }
}
