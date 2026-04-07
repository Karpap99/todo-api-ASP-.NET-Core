using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _db;
        public CategoryService(ICategoryRepository db)
        {
            _db = db;
        }

        public async Task<Category?> GetOneByID(Guid id)
        {
            return await _db.GetOneByID(id);
        }

        public async Task<List<Category>> GetAllAsync(PaginationParams pagination)
        {
            return await _db.GetAllAsync(pagination);
        }

        public async Task<Category?> GetByName(string name)
        {
            return await _db.GetByName(name);
        }

        public async Task<Category> Add(CreateCategoryDTO createCategoryDTO)
        {
            var categoryDuplicate = await _db.GetByName(createCategoryDTO.Name);

            if (categoryDuplicate != null) { 
                throw new Exception("Category with the same name already exists.");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = createCategoryDTO.Name
            };

            return await _db.Add(category);
        }

        public async Task<Category> Update(Category category)
        {
            return await _db.Update(category);
        }

        public Task Delete(Guid id)
        {
            return _db.Delete(id);
        }
    }
}
