namespace StudentProjects.Models.Request;

public record PostStudent(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? Phone,
    string? Email,
    string? Telegram,
    string? AcademicGroup,
    Guid? PositionId);