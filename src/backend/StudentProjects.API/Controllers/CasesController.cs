using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Exceptions;
using StudentProjects.API.Services;
using StudentProjects.ClientModels.Request;
using StudentProjects.ClientModels.Response;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/cases")]
public class CasesController(CaseService caseService, UserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Case>> PostAsync([FromBody] PostCase request)
    {
        var user = await userService.GetAuthorizedUserAsync(User.Claims);
        var createdCase = await caseService.AddAsync(request.Name, request.Description, user);

        return Ok(createdCase);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Case>>> GetAsync([FromQuery] CommonQuery request)
    {
        return Ok(await caseService.GetAsync(request.Offset, request.Limit));
    }

    [HttpDelete("{caseId:guid}")]
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
    public async Task<ActionResult<Case>> GetCaseAsync(Guid caseId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{caseId:guid}/reviews")]
    public async Task<IActionResult> PostReviewAsync([FromRoute] Guid caseId, [FromBody] PostReview request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{caseId:guid}/reviews")]
    public async Task<ActionResult<ICollection<Review>>> GetReviewsAsync(
        [FromRoute] Guid caseId,
        [FromQuery] CommonQuery request)
    {
        throw new NotImplementedException();
    }
}