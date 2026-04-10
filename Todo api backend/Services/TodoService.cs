using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.DTOs.TodoDtos;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoService: ITodoService
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

        public async Task<TodoResponseDTO?> GetOneByID(Guid id, Guid userId) {
            var todo = await _repo.GetOneByIdWithCategories(id);
            if (todo == null) return null;

            return new TodoResponseDTO(todo)
            {
                Categories = todo.TodoCategories.Select(tc => new CategoryResponseDTO(tc.Category)).ToList(),
            };
        }


        public async Task<List<TodoResponseDTO>> GetAllAsync(Guid userId) {
            var todos = await _repo.GetByAuthorIdWithCategories(userId);
            return todos.Select(
                todo => new TodoResponseDTO(todo) 
                {
                    Categories = todo.TodoCategories.Select(tc => new CategoryResponseDTO(tc.Category)).ToList(),
                }).ToList(); 
        }


        public async Task<PaginatedResponse<TodoResponseDTO>> GetPaginatedAsync(PaginationParams pagination, Guid userId) {
            var(todos, total) = await _repo.GetByAuthorIdWithCategoriesPaginated(pagination, userId);


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

        public async Task<TodoResponseDTO?> GetByNameAsync(string name, Guid userGuid) {
            var todo = await _repo.GetByName(name);
            if (todo == null) return null;

            return new TodoResponseDTO(todo);
        }


        public async Task<PaginatedResponse<TodoResponseDTO>> GetPartialSearchByTitleAsync(string title, PaginationParams pagination, Guid userId) {
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
        public async Task<TodoResponseDTO?> AddAsync(CreateTodoDTO createTodoTaskDTO, Guid userId) {

            var todo = new Todo
            {
                Title = createTodoTaskDTO.Title,
                Description = createTodoTaskDTO.Description,
                IsCompleted = false,
                AuthorId = userId,
            };

            var result = await _repo.AddAsync(todo);

            if(createTodoTaskDTO.Categories != null && createTodoTaskDTO.Categories.Count > 0) {
                var validCategoryIds = await _categoryService.ValidateCategoryIdsAsync(createTodoTaskDTO.Categories);
                await _todoCategoryService.AddManyAsync(validCategoryIds, result.Id);
            }

            return new TodoResponseDTO(result);
        }

        public async Task<TodoResponseDTO?> UpdateAsync(UpdateTodoDTO updateTodoTaskDTO, Guid userId) => null;

        public async Task DeleteAsync(Guid id, Guid userId) => await _repo.DeleteAsync(id);
    }
}
