using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo_api_backend.DTOs;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class BaseProtectedRepository<T> : IBaseProtectedRepository<T> where T : BaseUserEntity
    {
        protected readonly ApplicationDbContext _db;



        public BaseProtectedRepository(ApplicationDbContext db)
        {
            _db = db;
        }
         
        public async Task<T?> GetOneByID(Guid id, Guid userId)
        {
            return await _db.Set<T>().FirstAsync(x => x.Id == id && x.AuthorId == userId);
        }



        public async Task<List<T>> GetAllAsync(Guid userId)
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetPaginatedAsync(
            PaginationParams pagination,
            Expression<Func<T, object>> orderBy,
            Guid userId
           )
        {
            var position = (pagination.Page - 1) * pagination.Limit;

            return await _db.Set<T>()
                .OrderBy(orderBy)
                .Skip(position)
                .Take(pagination.Limit)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(Guid userId)
        {
            return await _db.Set<T>().CountAsync();
        }

        public async Task<T> AddAsync(T item)
        {
            await _db.Set<T>().AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item, Guid userId)
        {
            _db.Set<T>().Update(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var entity = await _db.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id && x.AuthorId == userId);

            if (entity == null) return false;

            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
