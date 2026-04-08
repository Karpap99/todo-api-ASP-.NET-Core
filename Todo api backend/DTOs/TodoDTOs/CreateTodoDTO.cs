using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.TodoTask
{
    public class CreateTodoDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
