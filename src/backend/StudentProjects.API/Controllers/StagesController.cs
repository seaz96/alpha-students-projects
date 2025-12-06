using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Route("v1/stages")]
[ApiController]
public class StagesController(StagesService stagesService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Stage>> PostAsync([FromBody] PostStage request)
    {
        //todo(azanov.n): лучше бы в 201 Created
        return Ok(await stagesService.CreateAsync(request));
    }

    [HttpPatch("{stageId:guid}")]
    public async Task<ActionResult<Stage>> PatchAsync([FromRoute] Guid stageId, [FromBody] PatchStage request)
    {
        return Ok(await stagesService.UpdateAsync(stageId, request));
    }

    [HttpGet]
    public async Task<ActionResult<QueryResponse<Stage>>> GetAsync([FromQuery] QueryStages request)
    {
        return Ok(await stagesService.QueryAsync(request));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Stage>> GetAsync([FromRoute] Guid id)
    {
        return Ok(await stagesService.GetAsync(id));
    }
}