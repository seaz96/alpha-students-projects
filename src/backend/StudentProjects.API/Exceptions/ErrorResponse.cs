using System.Net;

namespace StudentProjects.API.Exceptions;

public record ErrorResponse(string Type, HttpStatusCode StatusCode = HttpStatusCode.InternalServerError, string Message = "");