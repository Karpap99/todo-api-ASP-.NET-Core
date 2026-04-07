using Microsoft.EntityFrameworkCore;
using Todo_api_backend.DTOs;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category?> GetOneByID(Guid id) => await _db.Categories.FindAsync(id);

        public async Task<Category?> GetByName(string name)
        {
            return await _db.Categories.Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetAllAsync(PaginationParams paginationParams)
        {
            var position = (paginationParams.Page - 1) * paginationParams.Limit;
            return await _db.Categories.OrderBy(b => b.Id).Skip(position).Take(paginationParams.Limit).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync() => await _db.Categories.CountAsync();

        public async Task<Category> Add(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task Delete(Guid id)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
            }
        }
    }
}
