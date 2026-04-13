using Todo_api_backend.DTOs;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseProtectedRepository<Category>
    {
        public Task<Category?> GetByName(string name);

        public Task<List<Category>> GetByIds(List<Guid> categoryIds);

        public Task<(List<Category>, int total)> GetPartialSearchByTitlePaginated(string title, PaginationParams pagination, Guid authorId);

        public Task<(List<Category>, int total)> GetByQuery(PaginationParams pagination, Guid authorId, string? query = null);

        public Task<bool> GetOneByNameAndAuthor(string name, Guid authorId);
    }
}
