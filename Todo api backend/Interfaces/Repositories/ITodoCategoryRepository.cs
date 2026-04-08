using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ITodoCategoryRepository: IBaseRepository<TodoCategory>
    {
        public Task AddManyAsync(List<Guid> categories, Guid todoId);
    }
}
