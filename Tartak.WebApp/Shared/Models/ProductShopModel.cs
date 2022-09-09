using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tartak.WebApp.Shared.Models
{
    public class ProductShopModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopId { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        [JsonPropertyName("quantity")]
        public uint QuantityInShop { get; set; }

    }
}
