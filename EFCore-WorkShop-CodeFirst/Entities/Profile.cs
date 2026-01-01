using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EFCore_WorkShop_CodeFirst.Entities
{
    internal class Profile
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string? FullName { get; set; }

        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // one-to-one relationship with User
        // Foreign Key and navigation property
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}
