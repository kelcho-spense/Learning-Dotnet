using EFCore_WorkShop_CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace EFCore_WorkShop_CodeFirst.Data
{
    internal class BloggingContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Example connection string — change as needed
            options.UseSqlServer(@"Server=localhost;Initial Catalog=blogging_db;User ID=SA;Password=pass;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One relationship (User - Profile)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-One relationship (User - Author)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Author)
                .WithOne(a => a.User)
                .HasForeignKey<Author>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many relationship (Author - Blogs)
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many relationship (Blog - Comments)
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Blog)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many relationship (User - Comments)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many relationship (Blog - Category)
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Blogs)
                .UsingEntity(j => j.ToTable("BlogCategories"));
        }
    }
}
