namespace StudentProjects.Models.Request;

public record StudentPatch(
    Guid? Id,
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? Phone,
    string? Email,
    string? Telegram,
    string? AcademicGroup,
    Guid? PositionId);