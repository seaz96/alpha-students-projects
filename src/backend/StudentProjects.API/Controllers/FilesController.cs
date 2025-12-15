using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/files")]
public class FilesController(FilesService filesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FileObject>>> GetFilesAsync([FromQuery] Guid teamId)
    {
        return Ok(await filesService.GetFilesAsync(teamId));
    }

    [HttpGet("{teamId:guid}/{name}/content-url")]
    public async Task<IActionResult> GetFileContentUrl([FromRoute] Guid teamId, [FromRoute] string name)
    {
        return Ok(await filesService.GenerateGetPresignedUrl(teamId, name));
    }

    [HttpGet("{teamId:guid}/{name}/upload-url")]
    public async Task<IActionResult> GetPutPresignedUrlAsync([FromRoute] Guid teamId, [FromRoute] string name)
    {
        return Ok(await filesService.GeneratePutPresignedUrl(teamId, name));
    }

    [HttpPost("{teamId:guid}/{name}")]
    public async Task<IActionResult> CreateInfoAsync([FromRoute] Guid teamId, [FromRoute] string name)
    {
        return Ok(await filesService.CreateFileAsync(teamId, name));
    }

    [HttpDelete("{fileId:guid}")]
    public async Task<IActionResult> DeleteFileAsync([FromRoute] Guid fileId)
    {
        return Ok(await filesService.DeleteFileAsync(fileId));
    }
}