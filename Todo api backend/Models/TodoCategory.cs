namespace Todo_api_backend.Models
{
    public class TodoCategory : BaseEntity
    {

        public Guid TodoId { get; set; }

        public Todo Todo { get; set; } = null!;

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
