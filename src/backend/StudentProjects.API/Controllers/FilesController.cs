using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/files")]
public class FilesController(FilesService filesService, TeamsService teamsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FileObject>>> GetFilesAsync([FromQuery] Guid teamId)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(await filesService.GetFilesAsync(teamId));
    }

    [HttpGet("{teamId:guid}/{name}/content-url")]
    public async Task<ActionResult<LinkResponse>> GetFileContentUrl([FromRoute] Guid teamId, [FromRoute] string name)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(new LinkResponse(await filesService.GenerateGetPresignedUrl(teamId, name)));
    }

    [HttpGet("{teamId:guid}/{name}/upload-url")]
    public async Task<ActionResult<LinkResponse>> GetPutPresignedUrlAsync([FromRoute] Guid teamId, [FromRoute] string name)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(new LinkResponse(await filesService.GeneratePutPresignedUrl(teamId, name)));
    }

    [HttpPost("{teamId:guid}/{name}")]
    public async Task<IActionResult> CreateInfoAsync([FromRoute] Guid teamId, [FromRoute] string name)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(await filesService.CreateFileAsync(teamId, name));
    }

    [HttpDelete("{fileId:guid}")]
    public async Task<IActionResult> DeleteFileAsync([FromRoute] Guid fileId)
    {
        return Ok(await filesService.DeleteFileAsync(fileId));
    }
}