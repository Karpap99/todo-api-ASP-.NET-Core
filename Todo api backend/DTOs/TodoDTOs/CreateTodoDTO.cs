using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.TodoDtos
{
    public class CreateTodoDTO
    {

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public List<Guid> Categories { get; set; } = new List<Guid>();
    }
}
