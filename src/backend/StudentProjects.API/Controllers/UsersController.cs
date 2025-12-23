using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Configuration;
using StudentProjects.API.Utility;
using StudentProjects.Application.Services;
using StudentProjects.Domain.Enums;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/users")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserAccount>> LoginAsync([FromBody] LoginUser request)
    {
        var user = await userService.LoginAsync(request);
        SetAuthCookie(user);
        return Ok(user);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserAccount>> RegisterAsync([FromBody] RegisterUser request)
    {
        var user = await userService.RegisterAsync(request);
        SetAuthCookie(user);
        return Ok(user);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    public Task<ActionResult<UserAccount>> LogoutAsync()
    {
        RemoveAuthCookie();
        return Task.FromResult<ActionResult<UserAccount>>(Ok());
    }

    [HttpGet("current")]
    [Authorize]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<UserAccount>> GetInfoAsync()
    {
        var user = await userService.GetAuthorizedAccountAsync();
        return Ok(user);
    }

    [HttpPatch("current")]
    [Authorize]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<UserAccount>> PatchInfoAsync(PatchUser request)
    {
        return Ok(await userService.UpdateInfoAsync(request));
    }

    [HttpPatch("{userId:guid}/role")]
    [Authorize]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<UserAccount>> PatchRoleAsync([FromRoute] Guid userId, [FromQuery] UserRole role)
    {
        return Ok(await userService.UpdateRoleAsync(userId, role));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType<UserAccount>(StatusCodes.Status200OK)]
    [ApiConventionMethod(typeof(ProducesErrorsConvention), nameof(ProducesErrorsConvention.Common))]
    public async Task<ActionResult<IEnumerable<UserAccount>>> GetUsersAsync([FromQuery] QueryUserAccounts request)
    {
        return Ok(await userService.QueryAccountsAsync(request));
    }

    private void SetAuthCookie(UserAccount userAccount)
    {
        var token = AuthTokenMaker.GetAuthToken(userAccount);

        Response.Cookies.Append(AuthOptions.CookieName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.Add(AuthOptions.TokenLifetime)
        });
    }

    private void RemoveAuthCookie()
    {
        Response.Cookies.Append(AuthOptions.CookieName, "", new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.AddDays(-1)
        });
    }
}
