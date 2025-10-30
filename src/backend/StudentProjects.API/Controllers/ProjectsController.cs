using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/projects")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProjectResponse>> PostAsync([FromBody] PostProject request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{projectId:guid}")]
    public async Task<ActionResult<ProjectResponse>> GetAsync([FromRoute] Guid projectId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{projectId:guid}")]
    public async Task<ActionResult<ProjectResponse>> PatchAsync(
        [FromRoute] Guid projectId,
        [FromBody] PatchProject request)
    {
        throw new NotImplementedException();
    }
}