using Todo_api_backend.DTOs.Auth;
using Todo_api_backend.Models;

namespace Todo_api_backend.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AuthResponseDTO?> LoginAsync(AuthLoginDTO userDto);
        public Task<AuthResponseDTO?> RegisterAsync(AuthRegisterDTO userDto);

        public Task<string> GetToken(User user); 

    }
}
