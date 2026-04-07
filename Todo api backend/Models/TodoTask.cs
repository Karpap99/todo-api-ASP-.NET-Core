namespace Todo_api_backend.Models
{
    public class TodoTask
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }

        public User Author { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<TodoTaskCategory> TaskCategories { get; set; } = new List<TodoTaskCategory>();
    }
}
