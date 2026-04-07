using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetOneByID(Guid id);

        public Task<List<User>> GetAllAsync();

        public Task<User?> GetByEmailAsync(string email);

        public Task<User> Add(User user);

        public Task<User> Update(User user);

        public Task Delete(Guid id);

    }
}
