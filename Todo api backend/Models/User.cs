namespace Todo_api_backend.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
    }
}
