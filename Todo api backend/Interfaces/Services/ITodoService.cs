using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.TodoDtos;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ITodoService: IBaseProtectedService<TodoResponseDTO, CreateTodoDTO, UpdateTodoDTO>
    {
        public Task<TodoResponseDTO?> GetByNameAsync(string name, Guid userGuid);

        public Task<PaginatedResponse<TodoResponseDTO>> GetPartialSearchByTitleAsync(string title, PaginationParams pagination, Guid userId);
    }
}
