using Todo_api_backend.DTOs.UserDtos;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface IUserService: IBaseService<UserResponseDTO, CreateUserDTO, UpdateUserDTO>
    {
        public Task<UserResponseDTO?> GetByEmailAsync(string email);
    }
}
