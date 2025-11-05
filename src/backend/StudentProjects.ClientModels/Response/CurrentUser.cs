using StudentProjects.Domain.Enums;

namespace StudentProjects.ClientModels.Response;

public record CurrentUser(
    Guid Id,
    string Email,
    UserRole Role,
    string FirstName,
    string LastName,
    string MiddleName);