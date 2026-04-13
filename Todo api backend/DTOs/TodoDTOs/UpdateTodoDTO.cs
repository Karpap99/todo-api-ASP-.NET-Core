using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.TodoDtos
{
    public class UpdateTodoDTO
    {
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string? Title { get; set; }

        public DateTimeOffset? deadline { get; set; }

        public bool? IsCompleted { get; set; }

        public List<Guid> Categories { get; set; } = new List<Guid>();
    }
}
