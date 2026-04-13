namespace Todo_api_backend.Models
{
    public class Todo : BaseUserEntity
    {
        public required string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public ICollection<TodoCategory> TodoCategories { get; set; } = new List<TodoCategory>();
    }
}
