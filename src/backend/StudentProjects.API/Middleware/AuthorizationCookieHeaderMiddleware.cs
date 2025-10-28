using Microsoft.AspNetCore.Authentication.JwtBearer;
using StudentProjects.API.Configuration;

namespace StudentProjects.API.Middleware;

public class RequestHeadersComplementaryMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var authToken = context.Request.Cookies[AuthOptions.CookieName];

        if (!string.IsNullOrEmpty(authToken))
        {
            context.Request.Headers.Authorization = $"{JwtBearerDefaults.AuthenticationScheme} {authToken}";
        }

        await next(context);
    }
}