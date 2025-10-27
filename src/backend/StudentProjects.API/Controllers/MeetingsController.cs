using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("api/v1/meetings")]
public class MeetingsController : ControllerBase
{
    [HttpPatch("{meetingId:guid}")]
    public async Task<IActionResult> PatchMeetingAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{meetingId:guid}")]
    public async Task<IActionResult> GetMeetingAsync(Guid meetingId)
    {
        throw new NotImplementedException();
    }
}