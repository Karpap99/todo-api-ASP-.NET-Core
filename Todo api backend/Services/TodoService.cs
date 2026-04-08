using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.TodoTask;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoService: ITodoService
    {
        private readonly ITodoRepository _repo;
        public TodoService(ITodoRepository repo, IUserService userService)
        {
            _repo = repo;
        }

        public async Task<TodoResponseDTO?> GetOneByID(Guid id, Guid userGuid) {
            var task = await _repo.GetOneByID(id);
            if (task == null) return null;

            return new TodoResponseDTO(null);
        }

        public async Task<PaginatedResponse<TodoResponseDTO>> GetAllAsync(Guid userGuid, PaginationParams pagination) {
            var tasks = await _repo.GetAllAsync();
            return new PaginatedResponse<TodoResponseDTO>
            {
                Items = tasks.Select(task => new TodoResponseDTO(task)).ToList(),
                TotalItems = tasks.Count,
                TotalPages = tasks.Count

            }; 
        } 
        public async Task<TodoResponseDTO?> GetByName(string name, Guid userGuid) {
            var task = await _repo.GetByName(name);
            if (task == null) return null;

            return new TodoResponseDTO(task);
        }

        public async Task<TodoResponseDTO?> Add(CreateTodoDTO createTodoTaskDTO, Guid userGuid) {

            var task = new Todo
            {
                Title = createTodoTaskDTO.Title,
                Description = createTodoTaskDTO.Description,
                IsCompleted = false,
                AuthorId = userGuid,
            };

            var result = await _repo.AddAsync(task);

            return new TodoResponseDTO(result);
        }

        public async Task<TodoResponseDTO?> Update(UpdateTodoDTO updateTodoTaskDTO, Guid userGuid) => null;

        public async Task Delete(Guid id, Guid userGuid) => await _repo.DeleteAsync(id);
    }
}
