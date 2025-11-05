namespace StudentProjects.ClientModels.Response;

public record Todo(Guid Id, bool Checked, string Content, Guid? ParentId, Guid MeetingId);