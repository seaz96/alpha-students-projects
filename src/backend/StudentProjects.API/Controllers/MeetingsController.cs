using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/meetings")]
public class MeetingsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{meetingId:guid}")]
    public async Task<IActionResult> PatchAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> QueryAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{meetingId:guid}")]
    public async Task<IActionResult> GetAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }
}