namespace Todo_api_backend.Models
{
    public class Todo : BaseEntity
    {
        public required string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }

        public User Author { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<TodoCategory> TodoCategories { get; set; } = new List<TodoCategory>();
    }
}
