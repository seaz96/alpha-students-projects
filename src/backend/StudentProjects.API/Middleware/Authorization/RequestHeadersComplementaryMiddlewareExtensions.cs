namespace StudentProjects.API.Middleware.Authorization;

public static class RequestHeadersComplementaryMiddlewareExtensions
{
    public static void UseRequestHeadersComplementaryMiddleware(this WebApplication app)
    {
        app.UseMiddleware<RequestHeadersComplementaryMiddleware>();
    }
}