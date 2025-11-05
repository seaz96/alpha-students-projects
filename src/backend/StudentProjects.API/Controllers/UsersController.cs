using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.API.Configuration;
using StudentProjects.API.Converters;
using StudentProjects.API.Exceptions;
using StudentProjects.API.Services;
using StudentProjects.API.Utility;
using StudentProjects.ClientModels.Request;
using StudentProjects.ClientModels.Response;
using StudentProjects.Domain.Enums;
using User = StudentProjects.Domain.Entities.User;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/users")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType<CurrentUser>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CurrentUser>> LoginAsync([FromBody] LoginUser request)
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
    [ProducesResponseType<CurrentUser>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CurrentUser>> RegisterAsync([FromBody] RegisterUser request)
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
    [ProducesResponseType<CurrentUser>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CurrentUser>> GetInfoAsync()
    {
        var user = await userService.GetAuthorizedUserAsync(User.Claims);
        return Ok(user.ToInfoResponse());
    }

    [HttpPatch("current")]
    [Authorize]
    [ProducesResponseType<CurrentUser>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CurrentUser>> PatchInfoAsync(PatchUser request)
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
    [ProducesResponseType<CurrentUser>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CurrentUser>> PatchRoleAsync([FromRoute] Guid userId, [FromQuery] UserRole role)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user is null)
            throw new UserNotFoundException();
        user.Role = role;
        await userService.PatchUserAsync(user);
        return Ok(user.ToInfoResponse());
    }

    [HttpGet]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType<CurrentUser>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<CurrentUser>>> GetUsersAsync([FromQuery] CommonQuery request)
    {
        var users = await userService.GetUsersAsync(request.Offset, request.Limit);
        return Ok(users.Select(x => x.ToInfoResponse()));
    }
}