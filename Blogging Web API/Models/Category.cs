using System.ComponentModel.DataAnnotations;

namespace small_ecommerce_api.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        // Navigation property (One-to-Many relationship)
        public ICollection<Product>? Products { get; set; }
    }
}
