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
            return new CategoryResponseDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryPageResponseDTO> GetAllAsync(PaginationParams pagination)
        {
            var total = await _db.GetTotalCountAsync();
            var pages = (int)Math.Ceiling((double)total / pagination.Limit);

            if (pages > 0 && pagination.Page <= pages)
            {
                var categories = await _db.GetAllAsync(pagination);

                return new CategoryPageResponseDTO
                {
                    Categories = categories.Select(item => new CategoryResponseDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    }).ToList(),
                    TotalPages = pages,
                    TotalItems = total

                };
            }

            return new CategoryPageResponseDTO
            {
                Categories = new List<CategoryResponseDTO>(),
                TotalPages = pages,
                TotalItems = total
            };
        }

  

        public async Task<CategoryResponseDTO?> GetByName(string name)
        {
            var category = await _db.GetByName(name);
            return new CategoryResponseDTO
            {
                Id = category.Id,
                Name = category.Name
            };
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
                Name = createCategoryDTO.Name
            };

            var result = await _db.Add(category);

            return new CategoryResponseDTO { 
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryResponseDTO> Update(UpdateCategoryDTO updateCategoryDTO)
        {
            var exists = await _db.GetOneByID(updateCategoryDTO.Id);

            if (exists == null) { 
                throw new Exception("Category not found.");
            }

            if (updateCategoryDTO.Name != exists.Name) {
                exists.Name = updateCategoryDTO.Name;
            }

            await _db.Update(exists);

            return new CategoryResponseDTO {
                Id = exists.Id,
                Name = exists.Name
            };
        }

        public Task Delete(Guid id)
        {
            return _db.Delete(id);
        }
    }
}
