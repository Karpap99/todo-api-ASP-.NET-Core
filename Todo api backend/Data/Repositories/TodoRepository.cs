using Microsoft.EntityFrameworkCore;
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
    }
}
