using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.DTOs.TodoDtos;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repo;
        private readonly ICategoryService _categoryService;
        private readonly ITodoCategoryService _todoCategoryService;
        public TodoService(ITodoRepository repo, ICategoryService categoryService, ITodoCategoryService todoCategoryService)
        {
            _repo = repo;
            _categoryService = categoryService;
            _todoCategoryService = todoCategoryService;
        }

        public async Task<TodoResponseDTO> GetOneByID(Guid id, Guid userId)
        {
            var todo = await _repo.GetOneByIdWithCategories(id);

            if (todo == null) throw new KeyNotFoundException();

            return new TodoResponseDTO(todo)
            {
                Categories = todo.TodoCategories.Select(tc => new CategoryResponseDTO(tc.Category)).ToList(),
            };
        }


        public async Task<List<TodoResponseDTO>> GetAllAsync(Guid userId)
        {
            var todos = await _repo.GetByAuthorIdWithCategories(userId);
            return todos.Select(
                todo => new TodoResponseDTO(todo)
                {
                    Categories = todo.TodoCategories.Select(tc => new CategoryResponseDTO(tc.Category)).ToList(),
                }).ToList();
        }


        public async Task<PaginatedResponse<TodoResponseDTO>> GetPaginatedAsync(PaginationParams pagination, Guid userId)
        {
            var (todos, total) = await _repo.GetByAuthorIdWithCategoriesPaginated(pagination, userId);


            return new PaginatedResponse<TodoResponseDTO>
            {
                Items = todos.Select(todo => new TodoResponseDTO(todo)
                {
                    Categories = todo.TodoCategories.Select(tc => new CategoryResponseDTO(tc.Category)).ToList(),
                }).ToList(),
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / pagination.Limit)
            };
        }

        public async Task<TodoResponseDTO?> GetByNameAsync(string name, Guid userGuid)
        {
            var todo = await _repo.GetByName(name);
            if (todo == null) return null;

            return new TodoResponseDTO(todo);
        }


        public async Task<PaginatedResponse<TodoResponseDTO>> GetPartialSearchByTitleAsync(string title, PaginationParams pagination, Guid userId)
        {
            var (todos, total) = await _repo.GetPartialSearchByTitlePaginated(title, pagination, userId);
            return new PaginatedResponse<TodoResponseDTO>
            {
                Items = todos.Select(todo => new TodoResponseDTO(todo)
                {
                    Categories = todo.TodoCategories.Select(tc => new CategoryResponseDTO(tc.Category)).ToList(),
                }).ToList(),
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / pagination.Limit)
            };
        }
        public async Task<TodoResponseDTO> AddAsync(CreateTodoDTO createTodoDTO, Guid userId)
        {
            if (createTodoDTO.Deadline.HasValue)
            {
                createTodoDTO.Deadline = createTodoDTO.Deadline.Value.ToUniversalTime();
            }
            var todo = new Todo
            {
                Title = createTodoDTO.Title,
                Deadline = createTodoDTO.Deadline,
                IsCompleted = false,
                AuthorId = userId,
            };

            var result = await _repo.AddAsync(todo);

            if (createTodoDTO.Categories != null && createTodoDTO.Categories.Count > 0)
            {
                var validCategoryIds = await _categoryService.ValidateCategoryIdsAsync(createTodoDTO.Categories);
                await _todoCategoryService.AddManyAsync(validCategoryIds, result.Id);
            }

            return new TodoResponseDTO(result);
        }

        public async Task<TodoResponseDTO> UpdateAsync(UpdateTodoDTO updateTodoTaskDTO, Guid userId)
        {
            var todo = await _repo.GetOneByIdAndUser(updateTodoTaskDTO.Id, userId);

            if (todo == null) throw new UnauthorizedAccessException();

            if (updateTodoTaskDTO.Title != null)

            {
                todo.Title = updateTodoTaskDTO.Title;
            }

            if (updateTodoTaskDTO.deadline.HasValue)
            {
                todo.Deadline = updateTodoTaskDTO.deadline.Value.ToUniversalTime();
            }
            if (updateTodoTaskDTO.IsCompleted != null)
            {
                todo.IsCompleted = updateTodoTaskDTO.IsCompleted.Value;
            }


            var result = await _repo.UpdateAsync(todo);

            if (updateTodoTaskDTO.Categories != null && updateTodoTaskDTO.Categories.Count > 0)
            {
                var validCategoryIds = await _categoryService.ValidateCategoryIdsAsync(updateTodoTaskDTO.Categories);
                await _todoCategoryService.AddManyAsync(validCategoryIds, result.Id);
            }
            return new TodoResponseDTO(result);
        }

        public async Task DeleteAsync(Guid id, Guid userId) => await _repo.DeleteAsync(id);
    }
}
