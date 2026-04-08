using Todo_api_backend.DTOs.TodoTask;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoTaskService: ITodoTaskService
    {
        private readonly ITodoTaskRepository _repo;
        private readonly IUserService _userService;

        public TodoTaskService(ITodoTaskRepository repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        public async Task<TodoTaskResponseDTO?> GetOneByID(Guid id, Guid userGuid) {
            var task = await _repo.GetOneByID(id);
            if (task == null) return null;

            return new TodoTaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<List<TodoTaskResponseDTO>> GetAllAsync(Guid userGuid) {
            var tasks = await _repo.GetAllAsync();
            return tasks.Select(task => new TodoTaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            }).ToList(); ;
        } 
        public async Task<TodoTaskResponseDTO?> GetByName(string name, Guid userGuid) {
            var task = await _repo.GetByName(name);
            if (task == null) return null;

            return new TodoTaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<TodoTaskResponseDTO?> Add(CreateTodoTaskDTO createTodoTaskDTO, Guid userGuid) {

            var task = new TodoTask
            {
                Id = Guid.NewGuid(),
                Title = createTodoTaskDTO.Title,
                Description = createTodoTaskDTO.Description,
                IsCompleted = false,
                AuthorId = userGuid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _repo.Add(task);

            return new TodoTaskResponseDTO
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                IsCompleted = result.IsCompleted,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt
            };
        }

        public async Task<TodoTaskResponseDTO?> Update(UpdateTodoTaskDTO updateTodoTaskDTO, Guid userGuid) => new TodoTaskResponseDTO { };

        public async Task Delete(Guid id, Guid userGuid) => await _repo.Delete(id);
    }
}
