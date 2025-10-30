using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
public class TodosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{todoId:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid todoId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] Guid meetingId)
    {
        throw new NotImplementedException();
    }
}