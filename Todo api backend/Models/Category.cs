namespace Todo_api_backend.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public ICollection<TodoTaskCategory> TaskCategories { get; set; } = new List<TodoTaskCategory>();
    }
}
