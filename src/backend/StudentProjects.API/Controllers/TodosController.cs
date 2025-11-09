using Microsoft.AspNetCore.Mvc;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Route("v1/todos")]
[ApiController]
public class TodosController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Todo>> PostAsync([FromBody] PostTodo request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{todoId:guid}")]
    public async Task<ActionResult<Todo>> GetByIdAsync([FromRoute] Guid todoId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Todo>>> GetAsync([FromQuery] Guid meetingId)
    {
        throw new NotImplementedException();
    }
}