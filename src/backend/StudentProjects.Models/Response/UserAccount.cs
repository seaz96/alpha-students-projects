using StudentProjects.Domain.Enums;

namespace StudentProjects.Models.Response;

public record UserAccount(
    Guid Id,
    string Email,
    UserRole Role,
    string FirstName,
    string LastName,
    string MiddleName);