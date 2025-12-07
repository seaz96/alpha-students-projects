using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/teams")]
public class TeamsController(TeamsService teamsService, StudentsService studentsService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Team>> PostAsync([FromBody] PostTeam request)
    {
        return Ok(await teamsService.CreateAsync(request));
    }

    [HttpGet("{teamId:guid}")]
    public async Task<ActionResult<Team>> GetAsync([FromRoute] Guid teamId)
    {
        return Ok(await teamsService.GetAsync(teamId));
    }

    [HttpGet]
    public async Task<ActionResult<QueryResponse<Team>>> QueryAsync([FromQuery] QueryTeams request)
    {
        return Ok(await teamsService.QueryAsync(request));  
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Team>> DeleteAsync([FromRoute] Guid id)
    {
        return Ok(await teamsService.DeleteAsync(id));
    }

    [HttpPatch("{teamId:guid}")]
    public async Task<ActionResult<Team>> PatchTeamAsync([FromRoute] Guid teamId, [FromBody] PatchTeam request)
    {
        return Ok(await teamsService.UpdateAsync(teamId, request));
    }

    [HttpPost("{teamId:guid}/students")]
    public async Task<ActionResult<ICollection<TeamStudent>>> AddStudentsAsync([FromRoute] Guid teamId, [FromBody] PostStudents request)
    {
        return Ok(await studentsService.AddTeamStudentsAsync(teamId, request));
    }

    [HttpPatch("{teamId:guid}/students")]
    public async Task<IActionResult> PatchStudentsAsync([FromRoute] Guid teamId, [FromBody] PatchStudents request)
    {
        return Ok(await studentsService.UpdateTeamStudentsAsync(teamId, request));
    }

    [HttpDelete("{teamId:guid}/students")]
    public async Task<IActionResult> DeleteStudentsAsync([FromRoute] Guid teamId, [FromBody] DeleteStudents request)
    {
        return Ok(await studentsService.DeleteTeamStudentsAsync(teamId, request));
    }

    //todo(azanov.n): подумать над тем, чтобы в отдельный контроллер вынести
    [HttpGet("{teamId:guid}/result")]
    public async Task<ActionResult<ResultMeta>> GetResultAsync([FromRoute] Guid teamId)
    {
        return Ok(await teamsService.GetResultAsync(teamId));
    }

    [HttpPut("{teamId:guid}/result")]
    public async Task<ActionResult<ResultMeta>> PutResultAsync([FromRoute] Guid teamId, [FromBody] PutTeamResult request)
    {
        return Ok(await teamsService.UpdateResultAsync(teamId, request));
    }

    //todo: this and lower
    [HttpPost("{teamId:guid}/folders")]
    public async Task<IActionResult> PostTeamFolderAsync(Guid teamId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{teamId:guid}/folders/{folderId:guid}")]
    public async Task<IActionResult> GetTeamFolderAsync(Guid teamId, Guid folderId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{teamId:guid}/files")]
    public async Task<IActionResult> PostTeamFileAsync(Guid teamId)
    {
        throw new NotImplementedException();
    }

    //todo(azanov.n): в отдельный контроллер?
    [HttpPost("{teamId:guid}/files/{folderId:guid}")]
    public async Task<IActionResult> GetTeamFileAsync(Guid teamId, Guid folderId)
    {
        throw new NotImplementedException();
    }
}