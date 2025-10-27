using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("api/v1/calendar")]
public class CalendarController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GetCalendarAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPost("{teamId:guid}")]
    public async Task<IActionResult> GetCalendarByTeamAsync(Guid teamId)
    {
        throw new NotImplementedException();
    }
}