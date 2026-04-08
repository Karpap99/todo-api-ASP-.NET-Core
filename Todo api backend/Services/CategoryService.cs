using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _db;
        public CategoryService(ICategoryRepository db)
        {
            _db = db;
        }

        public async Task<CategoryResponseDTO?> GetOneByID(Guid id)
        {
            var category = await _db.GetOneByID(id);
            return new CategoryResponseDTO(category);
        }

        public async Task<PaginatedResponse<CategoryResponseDTO>> GetAllAsync(PaginationParams pagination)
        {
            var total = await _db.GetTotalCountAsync();
            var pages = (int)Math.Ceiling((double)total / pagination.Limit);

            if (pages > 0 && pagination.Page <= pages)
            {
                var categories = await _db.GetPaginatedAsync(pagination, x => x.Id);

                return new PaginatedResponse<CategoryResponseDTO>
                {
                    Items = categories.Select(item => new CategoryResponseDTO(item)).ToList(),
                };
            }

            return new PaginatedResponse<CategoryResponseDTO>
            {
                Items = new List<CategoryResponseDTO>(),
                TotalPages = pages,
                TotalItems = total
            };
        }

  

        public async Task<CategoryResponseDTO?> GetByName(string name)
        {
            var category = await _db.GetByName(name);

            return new CategoryResponseDTO(category);
        }

        public async Task<CategoryResponseDTO> Add(CreateCategoryDTO createCategoryDTO)
        {
            var categoryDuplicate = await _db.GetByName(createCategoryDTO.Name);

            if (categoryDuplicate != null) { 
                throw new Exception("Category with the same name already exists.");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Title = createCategoryDTO.Name
            };

            var result = await _db.AddAsync(category);

            return new CategoryResponseDTO(result);
        }

        public async Task<CategoryResponseDTO> Update(UpdateCategoryDTO updateCategoryDTO)
        {
            var exists = await _db.GetOneByID(updateCategoryDTO.Id);

            if (exists == null) { 
                throw new Exception("Category not found.");
            }

            if (updateCategoryDTO.Name != exists.Title) {
                exists.Title = updateCategoryDTO.Name;
            }

            await _db.UpdateAsync(exists);

            return new CategoryResponseDTO(exists);
        }

        public Task Delete(Guid id)
        {
            return _db.DeleteAsync(id);
        }
    }
}
