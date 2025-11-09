using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Utility;
using StudentProjects.Application.Services;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/cases")]
public class CasesController(
    CaseService caseService,
    ReviewsService reviewsService)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Case>> PostAsync([FromBody] PostCase request)
    {
        return Ok(await caseService.AddAsync(request));
    }

    [HttpGet]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<Case>>> GetAsync([FromQuery] CommonQuery request)
    {
        return Ok(await caseService.GetAsync(request));
    }

    [HttpDelete("{caseId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteAsync(Guid caseId)
    {
        await caseService.DeleteAsync(caseId);
        return Ok();
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
    public async Task<ActionResult<ICollection<Review>>> GetReviewsAsync(
        [FromRoute] Guid caseId,
        [FromQuery] CommonQuery request)
    {
        return Ok(await reviewsService.QueryAsync(caseId, request));
    }
}