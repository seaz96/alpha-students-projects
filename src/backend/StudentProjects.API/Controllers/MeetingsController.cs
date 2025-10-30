using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/meetings")]
public class MeetingsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<MeetingResponse>> PostAsync([FromBody] PostMeeting request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{meetingId:guid}")]
    public async Task<ActionResult<MeetingResponse>> PatchAsync([FromRoute] Guid meetingId, [FromBody] PatchMeeting request)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<MeetingResponse>>> QueryAsync([FromQuery] QueryMeetings request)
    {
        throw new NotImplementedException();
    }

    //todo: здесь еще хочется тудушки вернуть
    [HttpGet("{meetingId:guid}")]
    public async Task<MeetingResponse> GetAsync([FromRoute] Guid meetingId)
    {
        throw new NotImplementedException();
    }
}