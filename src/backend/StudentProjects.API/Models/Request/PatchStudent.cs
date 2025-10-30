namespace StudentProjects.API.Models.Request;

public record PatchStudent(Guid? Id, string? Name, string? Phone, string? Email, string? Telegram, Guid? PositionId);