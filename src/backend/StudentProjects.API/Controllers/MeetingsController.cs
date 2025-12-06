using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/meetings")]
public class MeetingsController(MeetingsService meetingsService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Meeting>> PostAsync([FromBody] PostMeeting request)
    {
        return Ok(await meetingsService.CreateAsync(request));
    }

    [HttpPatch("{meetingId:guid}")]
    public async Task<ActionResult<Meeting>> PatchAsync([FromRoute] Guid meetingId, [FromBody] PatchMeeting request)
    {
        return Ok(await meetingsService.UpdateAsync(meetingId, request));
    }

    [HttpGet]
    public async Task<ActionResult<QueryResponse<Meeting>>> QueryAsync([FromQuery] QueryMeetings request)
    {
        return Ok(await meetingsService.QueryAsync(request));
    }

    //todo: здесь еще хочется тудушки вернуть
    [HttpGet("{meetingId:guid}")]
    public async Task<ActionResult<Meeting>> GetAsync([FromRoute] Guid meetingId)
    {
        return Ok(await meetingsService.GetAsync(meetingId));
    }
}