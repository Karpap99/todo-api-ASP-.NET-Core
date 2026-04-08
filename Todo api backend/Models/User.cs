namespace Todo_api_backend.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
