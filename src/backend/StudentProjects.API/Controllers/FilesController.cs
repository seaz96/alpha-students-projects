using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Utility;
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
    [ProducesResponseType<IEnumerable<FileObject>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<IEnumerable<FileObject>>> GetFilesAsync([FromQuery] Guid teamId)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(await filesService.GetFilesAsync(teamId));
    }

    [HttpGet("{teamId:guid}/content-url")]
    [ProducesResponseType<LinkResponse>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<LinkResponse>> GetFileContentUrl([FromRoute] Guid teamId, [FromQuery] string name)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(new LinkResponse(await filesService.GenerateGetPresignedUrl(teamId, name)));
    }

    [HttpGet("{teamId:guid}/upload-url")]
    [ProducesResponseType<LinkResponse>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<LinkResponse>> GetPutPresignedUrlAsync([FromRoute] Guid teamId, [FromQuery] string name)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(new LinkResponse(await filesService.GeneratePutPresignedUrl(teamId, name)));
    }

    [HttpPost("{teamId:guid}")]
    [ProducesResponseType<FileObject>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<FileObject>> CreateInfoAsync([FromRoute] Guid teamId, [FromQuery] string name)
    {
        if (!await teamsService.ExistsAsync(teamId))
            throw new TeamNotFoundException();
        return Ok(await filesService.CreateFileAsync(teamId, name));
    }

    [HttpDelete("{fileId:guid}")]
    [ProducesResponseType<FileObject>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<FileObject>> DeleteFileAsync([FromRoute] Guid fileId)
    {
        return Ok(await filesService.DeleteFileAsync(fileId));
    }
}