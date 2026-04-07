namespace Todo_api_backend.DTOs
{
    public class PaginationParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
