using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;

namespace StudentProjects.API.Controllers;

[Route("v1/stages")]
[ApiController]
public class StagesController : ControllerBase
{
    [HttpPatch]
    public async Task<ActionResult<StageResponse>> PostAsync([FromBody] PostStage request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{stageId:guid}")]
    public async Task<ActionResult<StageResponse>> PatchAsync([FromRoute] Guid stageId, [FromBody] PatchStage request)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<StageResponse>>> GetAsync([FromBody] QueryStages request)
    {
        throw new NotImplementedException();
    }
}