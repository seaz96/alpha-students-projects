namespace StudentProjects.API.Middleware.Exceptions;

public static class ExceptionToErrorMiddlewareExtensions
{
    public static void UseExceptionToErrorMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionToErrorMiddleware>();
    }
}