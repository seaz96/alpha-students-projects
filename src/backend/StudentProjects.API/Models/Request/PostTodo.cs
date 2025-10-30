namespace StudentProjects.API.Models.Request;

public record PostTodo(Guid MeetingId, string? Content, Guid? ParentId);