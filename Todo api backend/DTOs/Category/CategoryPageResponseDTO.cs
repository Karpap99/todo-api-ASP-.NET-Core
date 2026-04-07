namespace Todo_api_backend.DTOs.Category
{
    public class CategoryPageResponseDTO
    {
        public List<CategoryResponseDTO> Categories { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
