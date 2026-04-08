namespace Todo_api_backend.Models
{
    public class Category : BaseEntity
    {
        public required string Title { get; set; }

        public ICollection<TodoCategory> TodoCategories { get; set; } = new List<TodoCategory>();
    }
}
