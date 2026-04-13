using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<CategoryResponseDTO> GetOneByID(Guid id, Guid userId)
        {
            var category = await _db.GetOneByID(id, userId);
            if (category == null) throw new KeyNotFoundException();
            return new CategoryResponseDTO(category);
        }
        public async Task<List<CategoryResponseDTO>> GetAllAsync(Guid userId)
        {
            var categories = await _db.GetAllAsync(userId);
            return categories.Select(item => new CategoryResponseDTO(item)).ToList();
        }
        public async Task<PaginatedResponse<CategoryResponseDTO>> GetPaginatedAsync(PaginationParams pagination, Guid userId)
        {
            var total = await _db.GetTotalCountAsync(userId);
            var pages = (int)Math.Ceiling((double)total / pagination.Limit);

            if (pages > 0 && pagination.Page <= pages)
            {
                var categories = await _db.GetPaginatedAsync(pagination, x => x.Id, userId);

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
        public async Task<PaginatedResponse<CategoryResponseDTO>> GetByQuery(PaginationParams pagination, Guid userId, string? query = null)
        {
            var (categories, total) = await _db.GetByQuery(pagination, userId, query);

            return new PaginatedResponse<CategoryResponseDTO>
            {
                Items = new List<CategoryResponseDTO>(),
                TotalPages = 0,
                TotalItems = 0
            };
        }
        public async Task<CategoryResponseDTO> GetByName(string name)
        {
            var category = await _db.GetByName(name);
            
            if (category == null) throw new KeyNotFoundException();

            return new CategoryResponseDTO(category);
        }
        public async Task<CategoryResponseDTO> AddAsync(CreateCategoryDTO createCategoryDTO, Guid userId)
        {
            if (await _db.GetOneByNameAndAuthor(createCategoryDTO.Title, userId))
                throw new Exception("Already exist");

            var category = new Category
            {
                Title = createCategoryDTO.Title,
                AuthorId = userId,
            };

            var result = await _db.AddAsync(category);

            return new CategoryResponseDTO(result);
        }
        public async Task<CategoryResponseDTO> UpdateAsync(UpdateCategoryDTO updateCategoryDTO, Guid userId)
        {
            var exists = await _db.GetOneByID(updateCategoryDTO.Id, userId);

            if (exists == null) { 
                throw new Exception("Category not found.");
            }

            if (updateCategoryDTO.Name != exists.Title) {
                exists.Title = updateCategoryDTO.Name;
            }

            await _db.UpdateAsync(exists, userId);

            return new CategoryResponseDTO(exists);
        }
        public async Task<List<Guid>> ValidateCategoryIdsAsync(List<Guid> categoryIds)
        {
            var validCategories = await _db.GetByIds(categoryIds);
            var validCategoryIds = validCategories.Select(c => c.Id).ToList();
            return validCategoryIds;
        }
        public Task DeleteAsync(Guid id, Guid userId)
        {
            return _db.DeleteAsync(id, userId);
        }
    }
}
