using Microsoft.EntityFrameworkCore;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetOneByID(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Set<User>().ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> Add(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }


        public async Task Delete(Guid id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

    }
}
