using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Utility;
using StudentProjects.Application.Services;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/cases")]
public class CasesController(CaseService caseService, ReviewsService reviewsService) : ControllerBase
{
    [HttpPost]  
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Case>> PostAsync([FromBody] PostCase request)
    {
        return Ok(await caseService.AddAsync(request));
    }

    [HttpPatch("{caseId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Case>> PatchAsync([FromRoute] Guid caseId, [FromBody] PatchCase request)
    {
        return Ok(await caseService.PatchAsync(caseId, request));
    }

    [HttpPut("{caseId:guid}/type")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Case>> ChangeTypeAsync([FromRoute] Guid caseId, [FromBody] ChangeCaseType request)
    {
        return Ok(await caseService.UpdateStatusAsync(caseId, request.Type));
    }

    [HttpGet]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<QueryResponse<Case>>> GetAsync([FromQuery] QueryCases request)
    {
        return Ok(await caseService.GetAsync(request));
    }

    [HttpDelete("{caseId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Case>> DeleteAsync(Guid caseId)
    {
        return Ok(await caseService.DeleteAsync(caseId));
    }

    [HttpGet("{caseId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Case>> GetCaseAsync(Guid caseId)
    {
        var result = await caseService.GetAsync(caseId);
        return Ok(result);
    }

    [HttpPut("{caseId:guid}/reviews")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Review>> PutReviewAsync([FromRoute] Guid caseId, [FromBody] PutReview request)
    {
        var caseEntity = await caseService.GetAsync(caseId);
        if (caseEntity is null)
            throw new CaseNotFoundException();
        var userId = User.GetUserId();
        return Ok(await reviewsService.CreateOrUpdateAsync(caseId, userId, request.Comment ?? "", request.IsDislike));
    }

    [HttpGet("{caseId:guid}/reviews")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<QueryResponse<Review>>> GetReviewsAsync(
        [FromRoute] Guid caseId,
        [FromQuery] CommonQuery request)
    {
        return Ok(await reviewsService.QueryAsync(caseId, request));
    }
}