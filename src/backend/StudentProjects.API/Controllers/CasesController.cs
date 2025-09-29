using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController, Authorize, Route("api/v1/cases")]
public class CasesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostCaseAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCasesAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{caseId:guid}")]
    public async Task<IActionResult> GetCaseAsync()
    {
        throw new NotImplementedException();
    }
}