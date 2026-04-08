using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ITodoRepository: IBaseRepository<Todo>
    {
        public Task<Todo?> GetByName(string name);
    }
}
