using Microsoft.EntityFrameworkCore;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
