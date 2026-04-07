using Todo_api_backend.DTOs;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetOneByID(Guid id);

        public Task<List<Category>> GetAllAsync(PaginationParams paginationParams);

        public Task<Category?> GetByName(string name);

        public Task<Category> Add(Category category);

        public Task<Category> Update(Category category);

        public Task Delete(Guid id);
    }
}
