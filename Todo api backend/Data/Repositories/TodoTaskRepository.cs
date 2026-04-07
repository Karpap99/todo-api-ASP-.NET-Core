using Microsoft.EntityFrameworkCore;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly ApplicationDbContext _db;
        public TodoTaskRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TodoTask?> GetOneByID(Guid id)
        {
            return await _db.TodoTasks.FindAsync(id);
        }

        public async Task<TodoTask?> GetByName(string name)
        {
            return await _db.TodoTasks.Where(t => t.Title == name).SingleOrDefaultAsync();
        }

        public async Task<List<TodoTask>> GetAllAsync()
        {
            return await _db.TodoTasks.ToListAsync();
        }

        public async Task<TodoTask> Add(TodoTask todoTask)
        {
            _db.TodoTasks.Add(todoTask);
            await _db.SaveChangesAsync();
            return todoTask;
        }

        public async Task<TodoTask> Update(TodoTask todoTask)
        {
            _db.TodoTasks.Update(todoTask);
            await _db.SaveChangesAsync();
            return todoTask;
        }

        public async Task Delete(Guid id)
        {
            var todoTask = await _db.TodoTasks.FindAsync(id);

            if (todoTask != null)
            {
                _db.TodoTasks.Remove(todoTask);
                await _db.SaveChangesAsync();
            }
        }
    }
}
