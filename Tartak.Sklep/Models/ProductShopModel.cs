using System.ComponentModel.DataAnnotations;

namespace Tartak.Magazyn.Models
{
    public class ProductShopModel
    {
        [Key]
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
        public uint QuantityInShop { get; set; }

    }
}
