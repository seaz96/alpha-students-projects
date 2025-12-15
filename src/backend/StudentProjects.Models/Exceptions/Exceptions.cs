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

public class StageNotFoundException(string message = "Stage with specified id not found.")
    : ApiException("urn:error:stage-not-found", HttpStatusCode.NotFound, message);

public class MeetingNotFoundException(string message = "Meeting with specified id not found.")
    : ApiException("urn:error:meeting-not-found", HttpStatusCode.NotFound, message);

public class TodoNotFoundException(string message = "Todo with specified id not found.")
    : ApiException("urn:error:todo-not-found", HttpStatusCode.NotFound, message);

public class ResultMetaNotFoundException(string message = "Result meta with specified id not found.")
    : ApiException("urn:error:result-meta-not-found", HttpStatusCode.NotFound, message);

public class StudentPositionNotFoundException(string message = "Student position with specified id not found.")
    : ApiException("urn:error:student-position-not-found", HttpStatusCode.NotFound, message);
    
public class FileObjectNotUploaded()
    : ApiException("urn:error:file-object-not-uploaded", HttpStatusCode.NotFound, "File with specified name not found in storage.");

public class FileNotFoundException(string message = "File with specified id not found.")
    : ApiException("urn:error:file-not-found", HttpStatusCode.NotFound, message);