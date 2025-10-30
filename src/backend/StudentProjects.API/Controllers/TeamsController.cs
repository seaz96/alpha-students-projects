using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/teams")]
public class TeamsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        throw new NotImplementedException();    
    }

    [HttpPatch("{teamId:guid}")]
    public async Task<IActionResult> PatchTeamAsync(Guid teamId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{teamId:guid}/students")]
    public async Task<IActionResult> PatchStudentsAsync(Guid teamId)
    {
        throw new NotImplementedException();
    }
    
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