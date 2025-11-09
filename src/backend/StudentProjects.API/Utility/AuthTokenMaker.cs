using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using StudentProjects.API.Configuration;
using StudentProjects.Models.Response;

namespace StudentProjects.API.Utility;

public class AuthTokenMaker
{
    public static string GetAuthToken(UserAccount userAccount)
    {
        var userId = userAccount.Id.ToString();
        var role = userAccount.Role.ToString();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Role, role)
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            issuer: AuthOptions.TokenIssuer,
            audience: AuthOptions.TokenIssuer,
            expires: DateTime.UtcNow + AuthOptions.TokenLifetime,
            signingCredentials: new SigningCredentials(AuthOptions.SecurityKey, SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }
}