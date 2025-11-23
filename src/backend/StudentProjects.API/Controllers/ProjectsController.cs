using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/projects")]
public class ProjectsController(ProjectsService projectsService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Project>> PostAsync([FromBody] PostProject request)
    {
        return Ok(await projectsService.PostAsync(request));
    }

    [HttpGet("{projectId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Project>> GetAsync([FromRoute] Guid projectId)
    {
        return Ok(await projectsService.GetAsync(projectId));
    }

    [HttpGet]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<Project>>> GetAsync([FromQuery] CommonQuery request)
    {
        return Ok(await projectsService.QueryAsync(request.Offset, request.Limit));
    }

    [HttpPatch("{projectId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Project>> PatchAsync(
        [FromRoute] Guid projectId,
        [FromBody] PatchProject request)
    {
        return Ok(await projectsService.UpdateAsync(projectId, request));
    }
}