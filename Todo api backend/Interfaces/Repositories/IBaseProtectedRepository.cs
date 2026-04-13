using System.Linq.Expressions;
using Todo_api_backend.DTOs;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface IBaseProtectedRepository<T>
    {
        public Task<T?> GetOneByID(Guid id, Guid userId);

        public Task<List<T>> GetAllAsync(Guid userId);

        public Task<List<T>> GetPaginatedAsync(PaginationParams pagination, Expression<Func<T, object>> orderBy, Guid userId);

        public Task<int> GetTotalCountAsync(Guid userId);
        public Task<T> AddAsync(T item);

        public Task<T> UpdateAsync(T item, Guid userId);

        public Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
