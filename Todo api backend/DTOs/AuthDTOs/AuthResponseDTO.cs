using Todo_api_backend.DTOs.UserDtos;

namespace Todo_api_backend.DTOs.Auth
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }

        public UserResponseDTO User { get; set; }
    }
}
