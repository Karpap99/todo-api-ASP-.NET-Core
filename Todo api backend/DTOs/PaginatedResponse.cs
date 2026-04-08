namespace Todo_api_backend.DTOs
{
    public class PaginatedResponse<T>
    {
      public List<T> Items { get; set; }

      public int TotalItems { get; set; }

      public int TotalPages { get; set; }
    }
}
