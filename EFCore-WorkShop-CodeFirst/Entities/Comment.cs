

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_WorkShop_CodeFirst.Entities
{
    internal class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // many-to-one relationship with User & Blog
        // Foreign Keys and navigation properties
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public Guid BlogId { get; set; }
        [ForeignKey(nameof(BlogId))]
        public Blog Blog { get; set; } = null!;
    }
}
