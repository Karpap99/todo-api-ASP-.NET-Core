using Microsoft.EntityFrameworkCore;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class TodoTaskCategoryRepository: ITodoTaskCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public TodoTaskCategoryRepository(ApplicationDbContext db) 
        { 
            _db = db;
        }

        public async Task<TodoTaskCategory?> GetOneByID(Guid id)
        {
            return await _db.TodoTaskCategories.FindAsync(id);
        }


        public async Task<List<TodoTaskCategory>> GetAllAsync()
        {
            return await _db.TodoTaskCategories.ToListAsync();
        }

        public async Task<TodoTaskCategory> Add(TodoTaskCategory todoTaskCategory)
        {
            _db.TodoTaskCategories.Add(todoTaskCategory);
            await _db.SaveChangesAsync();
            return todoTaskCategory;
        }

        public async Task<TodoTaskCategory> Update(TodoTaskCategory todoTaskCategory)
        {
            _db.TodoTaskCategories.Update(todoTaskCategory);
            await _db.SaveChangesAsync();
            return todoTaskCategory;
        }

        public async Task Delete(Guid id)
        {
            var todoTaskCategory = await _db.TodoTaskCategories.FindAsync(id);

            if (todoTaskCategory != null)
            {
                _db.TodoTaskCategories.Remove(todoTaskCategory);
                await _db.SaveChangesAsync();
            }
        }
    }
}
