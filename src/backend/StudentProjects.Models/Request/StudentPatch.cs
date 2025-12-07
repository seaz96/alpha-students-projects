namespace StudentProjects.Models.Request;

public record StudentPatch(
    Guid? Id,
    string? FirstName,
    string? LastName,
    string? MiddleName,
    string? Phone,
    string? Email,
    string? Telegram,
    string? AcademicGroup,
    Guid? PositionId);