using System.ComponentModel.DataAnnotations;

namespace Todo_api_backend.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Title { get; set; }

    }
}
