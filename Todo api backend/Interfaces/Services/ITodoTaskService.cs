using Todo_api_backend.DTOs.TodoTask;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ITodoTaskService
    {
        public Task<TodoTaskResponseDTO?> GetOneByID(Guid id, Guid userGuid);
        public Task<List<TodoTaskResponseDTO>> GetAllAsync(Guid userGuid);
        public Task<TodoTaskResponseDTO?> GetByName(string name, Guid userGuid);
        public Task<TodoTaskResponseDTO?> Add(CreateTodoTaskDTO createTodoTaskDTO, Guid userGuid);
        public Task<TodoTaskResponseDTO?> Update(UpdateTodoTaskDTO updateTodoTaskDTO, Guid userGuid);
        public Task Delete(Guid id, Guid userGuid);
    }
}
