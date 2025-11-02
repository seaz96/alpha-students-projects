using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProjects.API.Configuration;
using StudentProjects.API.Exceptions;
using StudentProjects.API.Models.Request;
using StudentProjects.API.Models.Response;
using StudentProjects.API.Utility;
using StudentProjects.Dal;
using StudentProjects.Domain.Entities;
using StudentProjects.Domain.Enums;

namespace StudentProjects.API.Controllers;

[ApiController, Route("v1/users")]
public class UsersController(DataContext context) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserInfoResponse>> LoginAsync([FromBody] LoginUser request)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user is null)
            throw new UnauthorizedException();

        var passwordVerified = PasswordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);
        if (!passwordVerified)
            throw new UnauthorizedException();
        
        var token = AuthTokenMaker.GetAuthToken(user);

        Response.Cookies.Append(AuthOptions.CookieName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.Add(AuthOptions.TokenLifetime)
        });

        return Ok(new UserInfoResponse(
            user.Id,
            user.Email,
            user.Role,
            user.FirstName ?? "",
            user.LastName ?? "",
            user.MiddleName ?? ""));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserInfoResponse>> RegisterAsync([FromBody] RegisterUser request)
    {
        if (context.Users.Any(x => x.Email == request.Email))
            throw new EmailRegisteredException();

        var passwordHash = PasswordHasher.HashPassword(request.Password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = passwordHash
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var token = AuthTokenMaker.GetAuthToken(user);

        Response.Cookies.Append(AuthOptions.CookieName, token, new CookieOptions
        {
            HttpOnly = true,
            //todo: after https on site should turn on
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.Add(AuthOptions.TokenLifetime)
        });

        return Ok(new UserInfoResponse(user.Id, user.Email, user.Role, user.FirstName ?? "", user.LastName ?? "", user.MiddleName ?? ""));
    }

    [HttpGet("info")]
    [Authorize]
    public async Task<ActionResult<UserInfoResponse>> GetInfoAsync()
    {
        var user = await GetAuthorizedUserAsync();
        return Ok(new UserInfoResponse(user.Id, user.Email, user.Role, user.FirstName ?? "", user.LastName ?? "", user.MiddleName ?? ""));
    }

    [HttpPatch("info")]
    [Authorize]
    public async Task<ActionResult<UserInfoResponse>> PatchInfoAsync(PatchUser request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{userId:guid}/role")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<UserInfoResponse> PatchRoleAsync([FromRoute] Guid userId, [FromQuery] UserRole role)
    {
        throw new NotImplementedException();
    }

    [HttpGet("")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<List<UserInfoResponse>>> GetUsersAsync([FromQuery] CommonQuery request)
    {
        throw new NotImplementedException();
    }

    private async Task<User> GetAuthorizedUserAsync()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (userId is null)
            throw new UnauthorizedException("User identifier not specified.");
        var user = await context.Users.FindAsync(Guid.Parse(userId.Value));
        return user ?? throw new UnauthorizedException("User with specified identifier not found.");
    }
}