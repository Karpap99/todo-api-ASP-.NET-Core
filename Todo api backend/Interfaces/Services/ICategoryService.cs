using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;

namespace Todo_api_backend.Interfaces.Services
{
    public interface ICategoryService: IBaseService<CategoryResponseDTO, CreateCategoryDTO, UpdateCategoryDTO>
    {
        public Task<CategoryResponseDTO?> GetByName(string name);

        public Task<List<Guid>> ValidateCategoryIdsAsync(List<Guid> categoryIds);
    }
}
