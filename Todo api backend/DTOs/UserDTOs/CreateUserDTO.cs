using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.UserDtos
{
    public class CreateUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
