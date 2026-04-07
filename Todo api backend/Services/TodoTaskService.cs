using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class TodoTaskService: ITodoTaskService
    {
        private readonly ITodoTaskRepository _repo;

        public TodoTaskService(ITodoTaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<TodoTask?> GetOneByID(Guid id) => await _repo.GetOneByID(id);

        public async Task<List<TodoTask>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<TodoTask?> GetByName(string name) => await _repo.GetByName(name);

        public async Task<TodoTask> Add(TodoTask todoTask) => await _repo.Add(todoTask);

        public async Task<TodoTask> Update(TodoTask todoTask) => await _repo.Update(todoTask);

        public async Task Delete(Guid id) => await _repo.Delete(id);
    }
}
