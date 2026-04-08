using System.Linq.Expressions;
using Todo_api_backend.DTOs;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<T> GetOneByID(Guid id);

        public Task<List<T>> GetAllAsync();

        public Task<List<T>> GetPaginatedAsync(PaginationParams pagination, Expression<Func<T, object>> orderBy);

        public Task<int> GetTotalCountAsync();

        public Task<T> AddAsync(T item);

        public Task<T> UpdateAsync(T item);

        public Task DeleteAsync(Guid id);
    }
}
