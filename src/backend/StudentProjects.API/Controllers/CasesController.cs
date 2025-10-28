using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProjects.API.Data;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/cases")]
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
    public async Task<IActionResult> GetCasesAsync()
    {
        return Ok(await context.Cases.ToListAsync());
    }

    [HttpGet("{caseId:guid}")]
    public async Task<IActionResult> GetCaseAsync(Guid caseId)
    {
        return Ok(await context.Cases.FindAsync(caseId));
    }
}