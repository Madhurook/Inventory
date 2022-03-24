using System.ComponentModel.DataAnnotations;

namespace Inventory.Product.Models
{
    public class Item
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
