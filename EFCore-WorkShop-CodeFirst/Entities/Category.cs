using System.ComponentModel.DataAnnotations;

namespace EFCore_WorkShop_CodeFirst.Entities
{
    internal class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // many-to-many relationship with Blog
        public List<Blog> Blogs { get; set; } = new();
    }
}
