using Todo_api_backend.Models;

namespace Todo_api_backend.DTOs
{
    public class BaseResponseEntityDTO
    {
        public BaseResponseEntityDTO(BaseEntity entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
