using System.Net;
using StudentProjects.API.Exceptions;

namespace StudentProjects.API.Middleware.Exceptions;

public class ExceptionToErrorMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception) when(!context.Response.HasStarted)
        {
            ErrorResponse error = exception switch
            {
                UnauthorizedException ex => new("urn:error:unauthorized", ex.StatusCode, ex.Message),
                EmailRegisteredException ex => new("urn:error:unauthorized", ex.StatusCode, exception.Message),
                _ => new("urn:error:unknown", HttpStatusCode.InternalServerError, "Unhandled exception occured.")
            };

            context.Response.StatusCode = (int)error.StatusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
