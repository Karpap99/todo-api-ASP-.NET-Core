using Microsoft.EntityFrameworkCore;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Todo> Todos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<TodoCategory> TodoCategories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.Email).IsRequired();
                b.Property(u => u.PasswordHash).IsRequired();
            });
     
            modelBuilder.Entity<Todo>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Title).IsRequired();
                b.HasIndex(t => new { t.AuthorId, t.Title});
                b.HasIndex(t => new { t.AuthorId, t.CreatedAt});
                b.HasOne(t => t.Author).WithMany(u => u.Todos).HasForeignKey(t => t.AuthorId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);
                b.HasIndex(c => c.Id);
                b.HasIndex(c => c.Title).IsUnique();
                b.Property(c => c.Title).IsRequired();
            });

            modelBuilder.Entity<TodoCategory>(b =>
            {
                b.HasKey(tc => new { tc.TodoId, tc.CategoryId });
                b.HasIndex(tc => new { tc.TodoId, tc.CategoryId }).IsUnique();
                b.HasOne(tc => tc.Todo).WithMany(t => t.TodoCategories).HasForeignKey(tc => tc.TodoId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne(tc => tc.Category).WithMany(c => c.TodoCategories).HasForeignKey(tc => tc.CategoryId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}