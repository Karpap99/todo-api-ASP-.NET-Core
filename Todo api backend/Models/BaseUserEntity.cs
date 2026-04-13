namespace Todo_api_backend.Models
{
    public class BaseUserEntity : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = null!;
    }
}
