using StudentProjects.Domain.Enums;

namespace StudentProjects.API.Models.Response;

public record UserInfoResponse(
    Guid Id,
    string Email,
    UserRole Role,
    string FirstName,
    string LastName,
    string MiddleName);