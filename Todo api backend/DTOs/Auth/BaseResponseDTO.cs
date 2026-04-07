using Todo_api_backend.DTOs.User;

namespace Todo_api_backend.DTOs.Auth
{
    public class BaseResponseDTO
    {
        public string Token { get; set; }

        public UserResponseDTO User { get; set; }
    }
}
