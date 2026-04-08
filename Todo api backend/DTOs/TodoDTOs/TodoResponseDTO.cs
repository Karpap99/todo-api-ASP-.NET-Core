using Todo_api_backend.Models;

namespace Todo_api_backend.DTOs.TodoDtos
{
    public class TodoResponseDTO : BaseResponseEntityDTO
    {
        public TodoResponseDTO(Todo todo) : base(todo) { 
            Title = todo.Title;
            Description = todo.Description;
            IsCompleted = todo.IsCompleted;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
