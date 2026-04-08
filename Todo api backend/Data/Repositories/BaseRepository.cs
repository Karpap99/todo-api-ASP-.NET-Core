using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo_api_backend.DTOs;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<T?> GetOneByID(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }



        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetPaginatedAsync(
            PaginationParams pagination,
            Expression<Func<T, object>> orderBy)
        {
            var position = (pagination.Page - 1) * pagination.Limit;

            return await _db.Set<T>()
                .OrderBy(orderBy)
                .Skip(position)
                .Take(pagination.Limit)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _db.Set<T>().CountAsync();
        }

        public async Task<T> AddAsync(T item)
        {
            await _db.Set<T>().AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _db.Set<T>().Update(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _db.Set<T>().FindAsync(id);

            if (item != null)
            {
                _db.Set<T>().Remove(item);
                await _db.SaveChangesAsync();
            }
        }
    }
}
