using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<CategoryResponseDTO?> GetOneByID(Guid id);

        public Task<CategoryPageResponseDTO> GetAllAsync(PaginationParams pagination);

        public Task<CategoryResponseDTO?> GetByName(string name);

        public Task<CategoryResponseDTO> Add(CreateCategoryDTO createCategoryDTO);

        public Task<CategoryResponseDTO> Update(UpdateCategoryDTO updateCategoryDTO);
        public Task Delete(Guid id);
    }
}
