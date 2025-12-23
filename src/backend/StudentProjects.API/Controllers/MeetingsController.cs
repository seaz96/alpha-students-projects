using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Utility;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/meetings")]
public class MeetingsController(MeetingsService meetingsService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Meeting>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Meeting>> PostAsync([FromBody] PostMeeting request)
    {
        return Ok(await meetingsService.CreateAsync(request));
    }

    [HttpPatch("{meetingId:guid}")]
    [ProducesResponseType<Meeting>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Meeting>> PatchAsync([FromRoute] Guid meetingId, [FromBody] PatchMeeting request)
    {
        return Ok(await meetingsService.UpdateAsync(meetingId, request));
    }

    [HttpGet]
    [ProducesResponseType<QueryResponse<Meeting>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<QueryResponse<Meeting>>> QueryAsync([FromQuery] QueryMeetings request)
    {
        return Ok(await meetingsService.QueryAsync(request));
    }

    [HttpGet("{meetingId:guid}")]
    [ProducesResponseType<Meeting>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Meeting>> GetAsync([FromRoute] Guid meetingId)
    {
        return Ok(await meetingsService.GetAsync(meetingId));
    }
}