using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Converters;
using StudentProjects.API.Exceptions;
using StudentProjects.API.Services;
using StudentProjects.API.Utility;
using StudentProjects.ClientModels.Request;
using StudentProjects.ClientModels.Response;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/cases")]
public class CasesController(
    CaseService caseService,
    UserService userService,
    ReviewsService reviewsService)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Case>> PostAsync([FromBody] PostCase request)
    {
        var user = await userService.GetAuthorizedUserAsync(User.Claims);
        var createdCase = await caseService.AddAsync(request.Name, request.Description, user);

        return Ok(createdCase);
    }

    [HttpGet]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<Case>>> GetAsync([FromQuery] CommonQuery request)
    {
        return Ok(await caseService.GetAsync(request.Offset, request.Limit));
    }

    [HttpDelete("{caseId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteAsync(Guid caseId)
    {
        var @case = await caseService.GetAsync(caseId);
        if (@case is null)
            throw new CaseNotFoundException();
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (@case.Author.Id != Guid.Parse(userId!.Value))
            throw new ForbiddenException();
        await caseService.DeleteAsync(caseId);
        return Ok();
    }

    [HttpGet("{caseId:guid}")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Case>> GetCaseAsync(Guid caseId)
    {
        var response = await caseService.GetAsync(caseId);
        if (response is null)
            throw new CaseNotFoundException();
        
        return Ok(response);
    }

    [HttpPost("{caseId:guid}/reviews")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostReviewAsync([FromRoute] Guid caseId, [FromBody] PostReview request)
    {
        var caseEntity = await caseService.GetAsync(caseId);
        var userId = User.GetUserId();
        if (caseEntity is null)
            throw new CaseNotFoundException();
        var review = await reviewsService.CreateAsync(caseId, userId, request.Comment ?? "", request.IsDislike);
        return Ok(review.ToClientModel(caseEntity.Author));
    }

    [HttpGet("{caseId:guid}/reviews")]
    public async Task<ActionResult<ICollection<Review>>> GetReviewsAsync(
        [FromRoute] Guid caseId,
        [FromQuery] CommonQuery request)
    {
        throw new NotImplementedException();
    }
}