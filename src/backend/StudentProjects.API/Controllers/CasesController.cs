using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProjects.API.Data;
using StudentProjects.API.Models.Dtos;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/cases")]
public class CasesController(DataContext context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CaseResponse>> PostAsync([FromBody] PostCase request)
    {
        await context.Cases.AddAsync(new Case
        {
            Id = Guid.NewGuid(),
            AuthorId = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<CaseResponse>>> GetAsync([FromQuery] CommonQuery request)
    {
        return Ok(await context.Cases.ToListAsync());
    }

    [HttpDelete("{caseId:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid caseId)
    {
        return Ok(await context.Cases.FindAsync(caseId));
    }

    [HttpGet("{caseId:guid}")]
    public async Task<ActionResult<CaseResponse>> GetCaseAsync(Guid caseId)
    {
        return Ok(await context.Cases.FindAsync(caseId));
    }

    [HttpPost("{caseId:guid}/reviews")]
    public async Task<IActionResult> PostReviewAsync([FromRoute] Guid caseId, [FromBody] PostReview request)
    {
        return Ok(await context.Cases.FindAsync(caseId));
    }

    [HttpGet("{caseId:guid}/reviews")]
    public async Task<ActionResult<ICollection<ReviewDto>>> GetReviewsAsync(
        [FromRoute] Guid caseId,
        [FromQuery] CommonQuery request)
    {
        return Ok(await context.Cases.FindAsync(caseId));
    }
}