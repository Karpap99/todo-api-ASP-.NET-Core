using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> GetOneByID(Guid id) => await _repo.GetOneByID(id);

        public async Task<List<User>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<User?> GetByEmailAsync(string email) => await _repo.GetByEmailAsync(email);

        public async Task<User> Add(User user) => await _repo.AddAsync(user);

        public async Task<User> Update(User user) => await _repo.UpdateAsync(user);

        public async Task Delete(Guid id) => await _repo.DeleteAsync(id);
    }
}
