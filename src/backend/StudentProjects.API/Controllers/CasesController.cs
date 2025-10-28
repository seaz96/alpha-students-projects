using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Data;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Controllers;

[ApiController, Route("api/v1/cases")]
public class CasesController(DataContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostCaseAsync()
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
    public Task<IActionResult> GetCasesAsync()
    {
        return Task.FromResult<IActionResult>(new OkObjectResult(context.Cases.ToList()));
    }
    
    [HttpGet("{caseId:guid}")]
    public async Task<IActionResult> GetCaseAsync(Guid caseId)
    {
        throw new NotImplementedException();
    }
}