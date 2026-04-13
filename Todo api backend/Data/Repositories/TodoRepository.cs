using Microsoft.EntityFrameworkCore;
using Todo_api_backend.DTOs;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext db) : base(db) { }
        public async Task<Todo?> GetByName(string name)
        {
            return await _db.Todos.Where(t => t.Title == name).SingleOrDefaultAsync();
        }

        public async Task<List<Todo>> GetByAuthorIdWithCategories(Guid authorId)
        {
            return await _db.Todos.Where(t => t.AuthorId == authorId)
                            .Include(t => t.TodoCategories)
                            .ThenInclude(tc=> tc.Category)
                            .ToListAsync();
        }

        public async Task<(List<Todo>, int total)> GetByAuthorIdWithCategoriesPaginated(PaginationParams pagination, Guid authorId)
        {
            var query = _db.Todos.Where(t => t.AuthorId == authorId);
            var total = await query.CountAsync();
            var todos = await query
                            .OrderByDescending(x => x.CreatedAt)
                            .Include(t => t.TodoCategories)
                            .ThenInclude(tc => tc.Category)
                            .Skip((pagination.Page - 1) * pagination.Limit)
                            .Take(pagination.Limit).ToListAsync();

            return (todos, total);
        }

        public async Task<Todo?> GetOneByIdWithCategories(Guid id)
        {
            return await _db.Todos.Where(t => t.Id == id)
                            .Include(t => t.TodoCategories)
                            .ThenInclude(tc => tc.Category)
                            .SingleOrDefaultAsync();
        }


        public async Task<(List<Todo>, int total)> GetPartialSearchByTitlePaginated(string title, PaginationParams pagination, Guid authorId)
        {
            var query = _db.Todos.Where(t => t.AuthorId == authorId && t.Title.Contains(title));
            var total = await query.CountAsync();
            var todos = await query.Include(t => t.TodoCategories)
                            .ThenInclude(tc => tc.Category)
                            .Skip((pagination.Page - 1) * pagination.Limit)
                            .Take(pagination.Limit).ToListAsync();
            return (todos, total);
        }

        public async Task<Todo?> GetOneByIdAndUser(Guid id, Guid userId) => await _db.Todos.Where(t => t.Id == id && t.AuthorId == userId).FirstAsync();
        

    }
}
