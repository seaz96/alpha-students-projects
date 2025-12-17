using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Utility;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Route("v1/stages")]
[ApiController]
public class StagesController(StagesService stagesService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Stage>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Stage>> PostAsync([FromBody] PostStage request)
    {
        //todo(azanov.n): лучше бы в 201 Created
        return Ok(await stagesService.CreateAsync(request));
    }

    [HttpPatch("{stageId:guid}")]
    [ProducesResponseType<Stage>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Stage>> PatchAsync([FromRoute] Guid stageId, [FromBody] PatchStage request)
    {
        return Ok(await stagesService.UpdateAsync(stageId, request));
    }

    [HttpGet]
    [ProducesResponseType<QueryResponse<Project>>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<QueryResponse<Stage>>> GetAsync([FromQuery] QueryStages request)
    {
        return Ok(await stagesService.QueryAsync(request));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<Stage>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Stage>> GetAsync([FromRoute] Guid id)
    {
        return Ok(await stagesService.GetAsync(id));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType<Stage>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<Stage>> DeleteAsync([FromRoute] Guid id)
    {
        return Ok(await stagesService.DeleteAsync(id));
    }
}