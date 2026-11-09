using uy01.Models;

namespace uy01.Repositories
{
    public class TodoRepository : GenericRepository<Todo>
    {
        public TodoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
