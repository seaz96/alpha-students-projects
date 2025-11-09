using Microsoft.AspNetCore.Mvc;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/projects")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Project>> PostAsync([FromBody] PostProject request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{projectId:guid}")]
    public async Task<ActionResult<Project>> GetAsync([FromRoute] Guid projectId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{projectId:guid}")]
    public async Task<ActionResult<Project>> PatchAsync(
        [FromRoute] Guid projectId,
        [FromBody] PatchProject request)
    {
        throw new NotImplementedException();
    }
}