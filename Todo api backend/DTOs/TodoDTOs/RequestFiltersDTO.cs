namespace Todo_api_backend.DTOs.TodoDTOs
{
    public class RequestFiltersDTO
    {
        public bool? IsCompleted { get; set; }
        public bool? IsOverdue { get; set; }
        public bool OnlyNotOverdue { get; set; } = false;
    }
}
