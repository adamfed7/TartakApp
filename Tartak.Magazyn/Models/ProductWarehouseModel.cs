using System.ComponentModel.DataAnnotations;

namespace Tartak.Magazyn.Models
{
    public class ProductWarehouseModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        public uint QuantityInWarehouse { get; set; }

    }
}
