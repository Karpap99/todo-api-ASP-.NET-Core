using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.Auth
{
    public class AuthRegisterDTO
    {
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
              ErrorMessage = "Password must contain letters and numbers")]
        public required string Password { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [Compare("Password", ErrorMessage = "Password must match")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
              ErrorMessage = "Password must contain letters and numbers")]
        public required string ConfirmPassword { get; set; }

    }
}
