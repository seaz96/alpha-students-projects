using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Utility;
using StudentProjects.Application.Services;
using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/teams")]
public class TeamsController(TeamsService teamsService, StudentsService studentsService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Team>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Team>> PostAsync([FromBody] PostTeam request)
    {
        return Ok(await teamsService.CreateAsync(request));
    }

    [HttpGet("{teamId:guid}")]
    [ProducesResponseType<Team>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Team>> GetAsync([FromRoute] Guid teamId)
    {
        return Ok(await teamsService.GetAsync(teamId));
    }

    [HttpGet]
    [ProducesResponseType<QueryResponse<Team>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<QueryResponse<Team>>> QueryAsync([FromQuery] QueryTeams request)
    {
        return Ok(await teamsService.QueryAsync(request));  
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType<Team>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Team>> DeleteAsync([FromRoute] Guid id)
    {
        return Ok(await teamsService.DeleteAsync(id));
    }

    [HttpPatch("{teamId:guid}")]
    [ProducesResponseType<Team>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Team>> PatchTeamAsync([FromRoute] Guid teamId, [FromBody] PatchTeam request)
    {
        return Ok(await teamsService.UpdateAsync(teamId, request));
    }

    [HttpPost("{teamId:guid}/students")]
    [ProducesResponseType<ICollection<TeamStudent>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<ICollection<TeamStudent>>> AddStudentsAsync([FromRoute] Guid teamId, [FromBody] PostStudents request)
    {
        return Ok(await studentsService.AddTeamStudentsAsync(teamId, request));
    }

    [HttpPatch("{teamId:guid}/students")]
    [ProducesResponseType<ICollection<TeamStudent>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<ICollection<TeamStudent>>> PatchStudentsAsync([FromRoute] Guid teamId, [FromBody] PatchStudents request)
    {
        return Ok(await studentsService.UpdateTeamStudentsAsync(teamId, request));
    }

    [HttpDelete("{teamId:guid}/students")]
    [ProducesResponseType<ICollection<TeamStudent>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<ICollection<TeamStudent>>> DeleteStudentsAsync([FromRoute] Guid teamId, [FromBody] DeleteStudents request)
    {
        return Ok(await studentsService.DeleteTeamStudentsAsync(teamId, request));
    }

    //todo(azanov.n): подумать над тем, чтобы в отдельный контроллер вынести
    [HttpGet("{teamId:guid}/result")]
    [ProducesResponseType<ResultMeta>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<ResultMeta>> GetResultAsync([FromRoute] Guid teamId)
    {
        return Ok(await teamsService.GetResultAsync(teamId));
    }

    [HttpPut("{teamId:guid}/result")]
    [ProducesResponseType<ResultMeta>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<ResultMeta>> PutResultAsync([FromRoute] Guid teamId, [FromBody] PutTeamResult request)
    {
        return Ok(await teamsService.UpdateResultAsync(teamId, request));
    }
}