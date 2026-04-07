using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoTaskCategoryService: ITodoTaskCategoryService
    {

        private readonly ITodoTaskCategoryRepository _repo;

        public TodoTaskCategoryService(ITodoTaskCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<TodoTaskCategory?> GetOneByID(Guid id) => await _repo.GetOneByID(id);

        public async Task<List<TodoTaskCategory>> GetAllAsync() => await _repo.GetAllAsync();


        public async Task<TodoTaskCategory> Add(TodoTaskCategory todoTaskCategory) => await _repo.Add(todoTaskCategory);

        public async Task<TodoTaskCategory> Update(TodoTaskCategory todoTaskCategory) => await _repo.Update(todoTaskCategory);

        public async Task Delete(Guid id) => await _repo.Delete(id);
    }
}
