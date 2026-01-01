using System.ComponentModel.DataAnnotations;


namespace EFCore_WorkShop_CodeFirst.Entities
{
    internal class User
    {
       public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Profile? Profile { get; set; }
        public Author? Author { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
