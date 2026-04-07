using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface ITodoTaskCategoryRepository
    {
        public Task<TodoTaskCategory?> GetOneByID(Guid id);

        public Task<List<TodoTaskCategory>> GetAllAsync();

        public Task<TodoTaskCategory> Add(TodoTaskCategory todoTaskCategory);

        public Task<TodoTaskCategory> Update(TodoTaskCategory todoTaskCategory);

        public Task Delete(Guid id);
    }
}
