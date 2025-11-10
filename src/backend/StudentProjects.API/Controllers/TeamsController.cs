using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/teams")]
public class TeamsController(TeamsService teamsService) : ControllerBase
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
    public async Task<ActionResult<ICollection<Team>>> QueryAsync([FromBody] QueryTeams request)
    {
        return Ok(await teamsService.QueryAsync(request));  
    }

    [HttpPatch("{teamId:guid}")]
    public async Task<ActionResult<Team>> PatchTeamAsync([FromRoute] Guid teamId, [FromBody] PatchTeam request)
    {
        return Ok(await teamsService.UpdateAsync(teamId, request));
    }

    [HttpPatch("{teamId:guid}/students")]
    public async Task<IActionResult> PatchStudentsAsync([FromRoute] Guid teamId, [FromBody] PatchStudents request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{teamId:guid}/result")]
    public async Task<IActionResult> PatchResultAsync([FromRoute] Guid teamId, [FromBody] PatchTeamResult request)
    {
        throw new NotImplementedException();
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