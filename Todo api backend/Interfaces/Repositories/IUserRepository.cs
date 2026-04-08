using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        public Task<User?> GetByEmailAsync(string email);
    }
}
