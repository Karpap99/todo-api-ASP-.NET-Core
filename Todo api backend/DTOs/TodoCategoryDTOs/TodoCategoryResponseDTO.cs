namespace Todo_api_backend.DTOs.TodoCategoryDTOs
{
    public class TodoCategoryResponseDTO : BaseResponseEntityDTO
    {
        public TodoCategoryResponseDTO(Models.TodoCategory todoCategory) : base(todoCategory) {
            TodoId = todoCategory.TodoId;
            CategoryId = todoCategory.CategoryId;
        }

        public Guid CategoryId { get; set; }
        public Guid TodoId { get; set; }
    }
}
