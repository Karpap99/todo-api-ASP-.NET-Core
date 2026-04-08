using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ITodoCategoryService
    {
        public Task<TodoCategory?> GetOneByID(Guid id);

        public Task<List<TodoCategory>> GetAllAsync();

        public Task<TodoCategory> Add(TodoCategory todoTaskCategory);

        public Task<TodoCategory> Update(TodoCategory todoTaskCategory);

        public Task Delete(Guid id);
    }
}
