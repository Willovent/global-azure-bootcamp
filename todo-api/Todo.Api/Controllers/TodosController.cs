using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Todo>> Get([FromServices] TodoContext todoContext)
        {
            return await todoContext.Todos.ToListAsync();
        }

        public async Task<Todo> Get([FromServices] TodoContext todoContext, int id)
        {
            return await todoContext.Todos.FindAsync(id);
        }

        [HttpPost]
        public async Task Post([FromServices] TodoContext todoContext, [FromBody] Todo value)
        {
            todoContext.Todos.Add(value);
            await todoContext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put([FromServices] TodoContext todoContext, int id, [FromBody] Todo value)
        {
            value.Id = id;
            todoContext.Todos.Update(value);
            await todoContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromServices] TodoContext todoContext, int id)
        {
            todoContext.Todos.Remove(new Todo { Id = id });
            await todoContext.SaveChangesAsync();
        }
    }
}
