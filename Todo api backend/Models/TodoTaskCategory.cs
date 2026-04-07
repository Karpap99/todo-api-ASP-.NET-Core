namespace Todo_api_backend.Models
{
    public class TodoTaskCategory
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public TodoTask Task { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
