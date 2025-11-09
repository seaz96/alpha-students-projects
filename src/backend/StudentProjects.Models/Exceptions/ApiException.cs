using System.Net;

namespace StudentProjects.Models.Exceptions;

public class ApiException(
    string type,
    HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
    string message = "") : Exception(message)

{
    public string Type { get; } = type;
    public HttpStatusCode StatusCode { get; } = statusCode;
}