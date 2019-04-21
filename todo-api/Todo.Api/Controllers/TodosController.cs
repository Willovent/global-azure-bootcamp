using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Core.Clients;
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
        public async Task<IEnumerable<Data.Todo>> Post([FromServices] TodoQueueClient todoQueueClient, [FromServices] TodoContext todoContext, [FromServices] ILogger<TodosController> logger, [FromBody] List<Data.Todo> todos)
        {
            var doneTodos = todos.Where(x => x.Done);
            var doneTodosIds = doneTodos.Select(x => x.Id);

            var newlyDoneTodos = await todoContext.Todos
                .Where(t => !t.Done)
                .Where(t => doneTodosIds.Contains(t.Id))
                .ToListAsync();

            newlyDoneTodos = newlyDoneTodos.Union(doneTodos.Where(x => x.Id == null)).ToList();

            foreach(var todo in newlyDoneTodos)
            {
                todoQueueClient.SendMessage(new Core.TodoActionMessageQueue
                {
                    TodoName = todo.Name,
                    CorrelationId = System.Diagnostics.Activity.Current.RootId
                });
            }

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
