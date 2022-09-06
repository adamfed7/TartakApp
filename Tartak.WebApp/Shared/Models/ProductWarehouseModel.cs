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
        [JsonPropertyName("purchaseprice")]
        public decimal PurchasePrice { get; set; }
        [JsonPropertyName("quantityinwarehouse")]
        public uint QuantityInWarehouse { get; set; }

    }
}
