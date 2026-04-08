using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.TodoTask;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ITodoService
    {
        public Task<TodoResponseDTO?> GetOneByID(Guid id, Guid userGuid);
        public Task<PaginatedResponse<TodoResponseDTO>> GetAllAsync(Guid userGuid, PaginationParams paginationParams);
        public Task<TodoResponseDTO?> GetByName(string name, Guid userGuid);
        public Task<TodoResponseDTO?> Add(CreateTodoDTO createTodoTaskDTO, Guid userGuid);
        public Task<TodoResponseDTO?> Update(UpdateTodoDTO updateTodoTaskDTO, Guid userGuid);
        public Task Delete(Guid id, Guid userGuid);
    }
}
