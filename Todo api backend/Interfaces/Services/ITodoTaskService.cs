using Todo_api_backend.DTOs.TodoTask;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ITodoTaskService
    {
        public Task<TodoTaskResponseDTO?> GetOneByID(Guid id);

        public Task<List<TodoTask>> GetAllAsync();

        public Task<TodoTask?> GetByName(string name);

        public Task<TodoTask> Add(TodoTask todoTask);

        public Task<TodoTask> Update(TodoTask todoTask);

        public Task Delete(Guid id);
    }
}
