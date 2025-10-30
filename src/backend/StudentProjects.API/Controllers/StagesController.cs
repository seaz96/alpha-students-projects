using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController]
public class StagesController : ControllerBase
{
    [HttpPatch]
    public async Task<IActionResult> PostAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{stageId:guid}")]
    public async Task<IActionResult> PatchAsync()
    {
        throw new NotImplementedException();
    }
}