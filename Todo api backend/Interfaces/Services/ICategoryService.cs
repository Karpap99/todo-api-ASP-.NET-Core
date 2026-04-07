using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<Category?> GetOneByID(Guid id);

        public Task<List<Category>> GetAllAsync(PaginationParams pagination);

        public Task<Category?> GetByName(string name);

        public Task<Category> Add(CreateCategoryDTO createCategoryDTO);

        public Task<Category> Update(Category category);

        public Task Delete(Guid id);
    }
}
