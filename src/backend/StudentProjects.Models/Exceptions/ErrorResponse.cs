using System.Net;

namespace StudentProjects.Models.Exceptions;

public record ErrorResponse(string Type, HttpStatusCode StatusCode = HttpStatusCode.InternalServerError, string Message = "");