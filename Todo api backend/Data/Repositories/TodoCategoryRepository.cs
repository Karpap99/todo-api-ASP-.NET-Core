using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class TodoCategoryRepository : BaseRepository<TodoCategory>, ITodoCategoryRepository
    {
        public TodoCategoryRepository(ApplicationDbContext db) : base(db) { }

        public async Task AddManyAsync(List<Guid> categories, Guid todoId)
        {
            var todoCategories = categories.Select(categoryId => new TodoCategory
            {
                TodoId = todoId,
                CategoryId = categoryId
            }).ToList();

            await _db.AddRangeAsync(todoCategories);
            await _db.SaveChangesAsync();
        }
    }
}
