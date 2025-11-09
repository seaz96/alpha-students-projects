using System.Security.Claims;
using StudentProjects.Models.Exceptions;

namespace StudentProjects.API.Utility;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userId, out var guid) ? guid : throw new UnauthorizedException();
    }
}