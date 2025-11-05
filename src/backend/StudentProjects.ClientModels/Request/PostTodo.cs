namespace StudentProjects.ClientModels.Request;

public record PostTodo(Guid MeetingId, string? Content, Guid? ParentId);