using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Data;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Data.Todo>> Get([FromServices] TodoContext todoContext)
        {
            return await todoContext.Todos.ToListAsync();
        }

        public async Task<Data.Todo> Get([FromServices] TodoContext todoContext, int id)
        {
            return await todoContext.Todos.FindAsync(id);
        }

        [HttpPost]
        public async Task<IEnumerable<Data.Todo>> Post([FromServices] TodoContext todoContext, [FromServices] ILogger<TodosController> logger, [FromBody] List<Data.Todo> todos)
        {
            todoContext.RemoveRange(todoContext.Todos);
            await todoContext.Todos.AddRangeAsync(todos);
            await todoContext.SaveChangesAsync();
            logger.LogInformation($"{todos.Count} Todos saved");
            return todos;
        }

        [HttpPut("{id}")]
        public async Task Put([FromServices] TodoContext todoContext, int id, [FromBody] Data.Todo value)
        {
            value.Id = id;
            todoContext.Todos.Update(value);
            await todoContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromServices] TodoContext todoContext, int id)
        {
            todoContext.Todos.Remove(new Data.Todo { Id = id });
            await todoContext.SaveChangesAsync();
        }
    }
}
