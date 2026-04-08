using Todo_api_backend.Models;

namespace Todo_api_backend.DTOs.UserDtos
{
    public class UserResponseDTO : BaseResponseEntityDTO
    {
        public UserResponseDTO(User user) : base(user) {
            Email = user.Email;
        }
        public string Email { get; set; }
    }
}
