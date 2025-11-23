using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Route("v1/todos")]
[ApiController]
public class TodosController(TodoService todoService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Todo>> PostAsync([FromBody] PostTodo request)
    {
        return Ok(await todoService.CreateAsync(request));
    }

    [HttpDelete("{todoId:guid}")]
    public async Task<ActionResult<Todo>> DeleteAsync([FromRoute] Guid todoId)
    {
        return Ok(await todoService.DeleteAsync(todoId));
    }

    [HttpPatch("{todoId:guid}")]
    public async Task<ActionResult<Todo>> PatchAsync([FromRoute] Guid todoId, [FromBody] PatchTodo request)
    {
        return Ok(await todoService.UpdateAsync(todoId, request));
    }
    
    [HttpGet("{todoId:guid}")]
    public async Task<ActionResult<Todo>> GetByIdAsync([FromRoute] Guid todoId)
    {
        return Ok(await todoService.GetByIdAsync(todoId));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Todo>>> GetAsync([FromQuery] Guid meetingId)
    {
        return Ok(await todoService.QueryAsync(meetingId));
    }
}