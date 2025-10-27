using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentProjects.API.Controllers;

[ApiController, Route("api/v1/users"), Authorize]
public class UsersController : ControllerBase
{
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> RegisterAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet("session")]
    public async Task<IActionResult> GetSessionAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPatch("info")]
    public async Task<IActionResult> PatchInfoAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{userId:guid}/permissions")]
    public async Task<IActionResult> PatchPermissions(Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUsersAsync()
    {
        throw new NotImplementedException();
    }
}