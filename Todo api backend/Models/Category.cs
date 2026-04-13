namespace Todo_api_backend.Models
{
    public class Category : BaseUserEntity
    {
        public required string Title { get; set; }
        public ICollection<TodoCategory> TodoCategories { get; set; } = new List<TodoCategory>();
    }
}
