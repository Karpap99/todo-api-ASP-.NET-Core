using Microsoft.EntityFrameworkCore;
using Todo_api_backend.DTOs;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Category?> GetByName(string name)
        {
            return await base._db.Categories.Where(c => c.Title == name).FirstOrDefaultAsync();
        }

    }
}
