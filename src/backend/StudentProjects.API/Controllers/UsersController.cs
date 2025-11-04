using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Configuration;
using StudentProjects.API.Converters;
using StudentProjects.API.Exceptions;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;
using StudentProjects.API.Services;
using StudentProjects.API.Utility;
using StudentProjects.Domain.Entities;
using StudentProjects.Domain.Enums;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/users")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserInfoResponse>> LoginAsync([FromBody] LoginUser request)
    {
        var user = await userService.GetUserByEmailAsync(request.Email);
        if (user is null)
            throw new UnauthorizedException();

        var passwordVerified = PasswordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);
        if (!passwordVerified)
            throw new UnauthorizedException();
        SetAuthCookie(user);

        return Ok(user.ToInfoResponse());
    }

    private void SetAuthCookie(User user)
    {
        var token = AuthTokenMaker.GetAuthToken(user);

        Response.Cookies.Append(AuthOptions.CookieName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.Add(AuthOptions.TokenLifetime)
        });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserInfoResponse>> RegisterAsync([FromBody] RegisterUser request)
    {
        if (await userService.GetUserByEmailAsync(request.Email) is not null)
            throw new EmailRegisteredException();

        var passwordHash = PasswordHasher.HashPassword(request.Password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = passwordHash
        };

        await userService.AddUserAsync(user);
        SetAuthCookie(user);

        return Ok(user.ToInfoResponse());
    }

    [HttpGet("current")]
    [Authorize]
    [ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserInfoResponse>> GetInfoAsync()
    {
        var user = await userService.GetAuthorizedUserAsync(User.Claims);
        return Ok(user.ToInfoResponse());
    }

    [HttpPatch("current")]
    [Authorize]
    [ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserInfoResponse>> PatchInfoAsync(PatchUser request)
    {
        var user = await userService.GetAuthorizedUserAsync(User.Claims);
        user.FirstName = request.FirstName; //todo: should I store this logic here?
        user.LastName = request.LastName;
        user.MiddleName = request.MiddleName;
        await userService.PatchUserAsync(user);
        return Ok(user.ToInfoResponse());
    }

    [HttpPatch("{userId:guid}/role")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserInfoResponse>> PatchRoleAsync([FromRoute] Guid userId, [FromQuery] UserRole role)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user is null)
            throw new UserNotFound();
        user.Role = role;
        await userService.PatchUserAsync(user);
        return Ok(user.ToInfoResponse());
    }

    [HttpGet]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserInfoResponse>>> GetUsersAsync([FromQuery] CommonQuery request)
    {
        var users = await userService.GetUsersAsync(request.Offset, request.Limit);
        return Ok(users.Select(x => x.ToInfoResponse()));
    }
}