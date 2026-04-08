using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoCategoryService: ITodoCategoryService
    {

        private readonly ITodoCategoryRepository _repo;

        public TodoCategoryService(ITodoCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<TodoCategory?> GetOneByID(Guid id) => await _repo.GetOneByID(id);

        public async Task<List<TodoCategory>> GetAllAsync() => await _repo.GetAllAsync();


        public async Task<TodoCategory> Add(TodoCategory todoTaskCategory) => await _repo.AddAsync(todoTaskCategory);

        public async Task<TodoCategory> Update(TodoCategory todoTaskCategory) => await _repo.UpdateAsync(todoTaskCategory);

        public async Task Delete(Guid id) => await _repo.DeleteAsync(id);
    }
}
