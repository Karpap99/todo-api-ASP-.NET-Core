using Todo_api_backend.DTOs;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ITodoRepository: IBaseRepository<Todo>
    {
        public Task<Todo?> GetByName(string name);
        public Task<List<Todo>> GetByAuthorIdWithCategories(Guid authorId);
        public Task<(List<Todo>, int total)> GetByAuthorIdWithCategoriesPaginated(PaginationParams pagination, Guid authorId);
        public Task<Todo?> GetOneByIdWithCategories(Guid id);
    }
}
