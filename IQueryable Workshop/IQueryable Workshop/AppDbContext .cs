using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryable_Workshop
{
    internal class AppDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Provide your connection string here
            optionsBuilder.UseSqlServer(
    @"Server=localhost;Database=student_db;User Id=SA;Password=pass;Encrypt=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configurations (optional/customizations)
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Gender).HasMaxLength(50).IsRequired();
                entity.ToTable("Student");
            });

            // Similarly you can set up configuration for  other entities
        }

    }
}
