using Microsoft.EntityFrameworkCore;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Models;

namespace Todo_api_backend.Data.Repositories
{
    public class TodoCategoryRepository: BaseRepository<TodoCategory>, ITodoCategoryRepository
    {
        public TodoCategoryRepository(ApplicationDbContext db) : base(db) { }
    }
}
