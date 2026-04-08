using Todo_api_backend.DTOs;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<Category?> GetByName(string name);

        public Task<List<Category>> GetByIds(List<Guid> categoryIds);

    }
}
