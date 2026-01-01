using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_WorkShop_CodeFirst.Entities
{
    internal class Blog
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public bool Published { get; set; } = false;
        public string? Excerpt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // many-to-one relationship with Author & comments
        // Navigation property
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;
        public List<Comment> Comments { get; set; } = new();

        // many-to-many relationship with Category
        public List<Category> Categories { get; set; } = new();
    }
}
