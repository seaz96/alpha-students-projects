using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController]
public class TodosController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TodoResponse>> PostAsync([FromBody] PostTodo request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{todoId:guid}")]
    public async Task<ActionResult<TodoResponse>> GetByIdAsync([FromRoute] Guid todoId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<TodosResponse>> GetAsync([FromQuery] Guid meetingId)
    {
        throw new NotImplementedException();
    }
}