namespace StudentProjects.Models.Request;

public record PostStudent(
    string? FirstName,
    string? LastName,
    string? MiddleName,
    string? Phone,
    string? Email,
    string? Telegram,
    string? AcademicGroup,
    Guid? PositionId);