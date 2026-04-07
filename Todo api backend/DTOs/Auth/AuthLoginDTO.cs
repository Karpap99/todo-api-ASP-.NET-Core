using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.Auth
{
    public class AuthLoginDTO
    {
        [EmailAddress]
        public required string Email { get; set; }

        [Required, Length(3, 255)]
        public required string Password { get; set; }

    }
}
