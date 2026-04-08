using Todo_api_backend.DTOs.TodoCategoryDTOs;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ITodoCategoryService: IBaseProtectedService<TodoCategoryResponseDTO, CreateTodoCategoryDTO, UpdateTodoCategoryDTO>
    {
        public Task AddManyAsync(List<Guid> categories, Guid todoId);
    }
}
