using Todo_api_backend.DTOs.Category;
using Todo_api_backend.Models;

namespace Todo_api_backend.DTOs.TodoDtos
{
    public class TodoResponseDTO : BaseResponseEntityDTO
    {
        public TodoResponseDTO(Todo todo) : base(todo) {
            Title = todo.Title;
            Deadline = todo.Deadline;
            IsCompleted = todo.IsCompleted;
        }

        public string Title { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public bool IsCompleted { get; set; }

        public List<CategoryResponseDTO> Categories { get; set; } = new List<CategoryResponseDTO>();
    }
}
