using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_WorkShop_CodeFirst.Entities
{
    internal class Author
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string? PenName { get; set; }

        public string? Biography { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // one-to-one relationship with User
        // Foreign Key and navigation property

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        // Add One‑to‑Many (1:n) relationships﻿
        // Navigation to Blogs
        public List<Blog> Blogs { get; set; } = new();
    }
}
