using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.TodoCategoryDTOs;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoCategoryService : ITodoCategoryService
    {

        private readonly ITodoCategoryRepository _repo;

        public TodoCategoryService(ITodoCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<TodoCategoryResponseDTO?> GetOneByID(Guid id, Guid userId) => null;

        public async Task<List<TodoCategoryResponseDTO>> GetAllAsync(Guid userId) => new List<TodoCategoryResponseDTO>();

        public async Task<PaginatedResponse<TodoCategoryResponseDTO>> GetPaginatedAsync(PaginationParams pagination, Guid userId) => new PaginatedResponse<TodoCategoryResponseDTO>();


        public async Task<TodoCategoryResponseDTO> AddAsync(CreateTodoCategoryDTO createTodoCategoryDTO, Guid userId) {
            var todoCategory = new TodoCategory
            {
                TodoId = createTodoCategoryDTO.TodoID,
                CategoryId = createTodoCategoryDTO.CategoryID
            };

           var createdTodoCategory = await _repo.AddAsync(todoCategory);

            return new TodoCategoryResponseDTO(createdTodoCategory);
        }


        public async Task AddManyAsync(List<Guid> categories, Guid todoId)
        {
            await _repo.AddManyAsync(categories, todoId);
        }

        // No sense to update a relationship between a todo and a category, we will just delete the old one and create a new one. Realization just to omit an error
        public async Task<TodoCategoryResponseDTO> UpdateAsync(UpdateTodoCategoryDTO updateTodoCategoryDTO, Guid userId) 
        {
            var todoCategory = new TodoCategory
            {
                TodoId = updateTodoCategoryDTO.TodoID,
                CategoryId = updateTodoCategoryDTO.CategoryID
            };

            var createdTodoCategory = await _repo.AddAsync(todoCategory);

            return new TodoCategoryResponseDTO(createdTodoCategory);
        }

        public async Task DeleteAsync(Guid id, Guid userId) => await _repo.DeleteAsync(id);
    }
}
