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

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    var now = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    var now = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

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
                b.HasIndex(c => new { c.AuthorId, c.Title }).IsUnique();
                b.HasOne(c => c.Author).WithMany(c => c.Categories).HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.Cascade);
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