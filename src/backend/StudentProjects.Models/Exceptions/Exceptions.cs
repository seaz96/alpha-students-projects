using System.Net;

namespace StudentProjects.Models.Exceptions;

public class ForbiddenException(string message = "Access forbidden.")
    : ApiException("urn:error:forbidden", HttpStatusCode.Forbidden, message);

public class UnauthorizedException(string message = "User not authorized.")
    : ApiException("urn:error:unauthorized", HttpStatusCode.Unauthorized, message);

public class EmailRegisteredException(string message = "User with specified email exists.")
    : ApiException("urn:error:email-registered", HttpStatusCode.Conflict, message);

public class UserNotFoundException(string message = "User with specified id not found.")
    : ApiException("urn:error:user-not-found", HttpStatusCode.NotFound, message);

public class CaseNotFoundException(string message = "Case with specified id not found.")
    : ApiException("urn:error:case-not-found", HttpStatusCode.NotFound, message);

public class ReviewNotFoundException(string message = "Review with specified id not found.")
    : ApiException("urn:error:review-not-found", HttpStatusCode.NotFound, message);

public class ProjectNotFoundException(string message = "Project with specified id not found.")
    : ApiException("urn:error:project-not-found", HttpStatusCode.NotFound, message);

public class TeamNotFoundException(string message = "Team with specified id not found.")
    : ApiException("urn:error:team-not-found", HttpStatusCode.NotFound, message);