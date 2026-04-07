using Microsoft.EntityFrameworkCore;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TodoTask> TodoTasks { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<TodoTaskCategory> TodoTaskCategories { get; set; } = null!;

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
     
            modelBuilder.Entity<TodoTask>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Title).IsRequired();
                b.HasOne(t => t.Author).WithMany(u => u.Tasks).HasForeignKey(t => t.AuthorId);
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Name).IsRequired();
            });

            modelBuilder.Entity<TodoTaskCategory>(b =>
            {
                b.HasKey(tc => new { tc.TaskId, tc.CategoryId });
                b.HasOne(tc => tc.Task).WithMany(t => t.TaskCategories).HasForeignKey(tc => tc.TaskId);
                b.HasOne(tc => tc.Category).WithMany(c => c.TaskCategories).HasForeignKey(tc => tc.CategoryId);
            });
        }
    }
}