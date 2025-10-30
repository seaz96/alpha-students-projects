using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StudentProjects.API.Configuration;

public static class AuthOptions
{
    public const string CookieName = "auth.token";
    public const string TokenIssuer = "Default";
    public const string TokenAudience = "Default";
    public static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(14);
    public static SymmetricSecurityKey SecurityKey { get; private set; } = null!;

    public static void Initialize(IConfiguration configuration)
    {
        var key = configuration.GetValue<string>("SECURITY_KEY")!;
        SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}