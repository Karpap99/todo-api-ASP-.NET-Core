using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<T> GetOneByID(Guid id);

        public Task<List<T>> GetAllAsync();

        public Task<T?> GetByName(string name);

        public Task<T> Add(Category category);

        public Task<T> Update(Category category);

        public Task Delete(Guid id);
    }
}
