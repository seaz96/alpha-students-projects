using System.Net;

namespace StudentProjects.API.Exceptions;

public class ForbiddenException(string message = "User not authorized.")
    : ApiException("urn:error:unauthorized", HttpStatusCode.Unauthorized, message);

public class UnauthorizedException(string message = "User not authorized.")
    : ApiException("urn:error:unauthorized", HttpStatusCode.Unauthorized, message);

public class EmailRegisteredException(string message = "User with specified email exists.")
    : ApiException("urn:error:email-registered", HttpStatusCode.Conflict, message);

public class UserNotFoundException(string message = "User with specified id not found.")
    : ApiException("urn:error:user-not-found", HttpStatusCode.NotFound, message);

public class CaseNotFoundException(string message = "Case with specified id not found.")
    : ApiException("urn:error:case-not-found", HttpStatusCode.NotFound, message);