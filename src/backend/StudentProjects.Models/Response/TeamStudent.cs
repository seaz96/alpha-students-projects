namespace StudentProjects.Models.Response;

public record TeamStudent(
    Guid Id,
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? Phone,
    string? Email,
    string? Telegram,
    StudentPosition? Position,
    string? AcademicGroup);