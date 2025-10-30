using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
[Route("v1/projects")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> PostAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{projectId:guid}")]
    public async Task<IActionResult> PatchAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{projectId:guid}/result")]
    public async Task<IActionResult> PatchResultAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }
}