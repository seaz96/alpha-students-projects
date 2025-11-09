using System.Net;
using StudentProjects.Models.Exceptions;
using ILogger = Serilog.ILogger;

namespace StudentProjects.API.Middleware.Exceptions;

public class ExceptionToErrorMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception) when(!context.Response.HasStarted)
        {
            ErrorResponse error = exception switch
            {
                UnauthorizedException ex => new(ex.Type, ex.StatusCode, ex.Message),
                EmailRegisteredException ex => new(ex.Type, ex.StatusCode, exception.Message),
                UserNotFoundException ex => new(ex.Type, ex.StatusCode, ex.Message),
                CaseNotFoundException ex => new(ex.Type, ex.StatusCode, ex.Message),
                ReviewNotFoundException ex => new(ex.Type, ex.StatusCode, ex.Message),
                _ => new("urn:error:unknown", HttpStatusCode.InternalServerError, "Unhandled exception occured.")
            };

            logger.Error(exception.ToString());
            context.Response.StatusCode = (int)error.StatusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
