using Todo_api_backend.DTOs.Auth;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<BaseResponseDTO?> LoginAsync(AuthLoginDTO userDto);
        public Task<BaseResponseDTO?> RegisterAsync(AuthRegisterDTO userDto);

        public Task<string> GetToken(User user); 

    }
}
