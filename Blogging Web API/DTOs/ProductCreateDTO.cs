using System.ComponentModel.DataAnnotations;

namespace small_ecommerce_api.DTOs
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null!;
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
    }
}
