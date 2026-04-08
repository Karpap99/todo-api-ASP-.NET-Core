namespace Todo_api_backend.DTOs.Category
{
    public class CategoryResponseDTO : BaseResponseEntityDTO
    {
        public CategoryResponseDTO(Models.Category category) : base(category) { 
            Title = category.Title;
        }
        public string Title { get; set; }
    }
}
