using Microsoft.AspNetCore.Mvc;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Route("v1/stages")]
[ApiController]
public class StagesController : ControllerBase
{
    [HttpPatch]
    public async Task<ActionResult<Stage>> PostAsync([FromBody] PostStage request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{stageId:guid}")]
    public async Task<ActionResult<Stage>> PatchAsync([FromRoute] Guid stageId, [FromBody] PatchStage request)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Stage>>> GetAsync([FromBody] QueryStages request)
    {
        throw new NotImplementedException();
    }
}