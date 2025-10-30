namespace StudentProjects.API.Models.Response;

public record TodoResponse(Guid Id, bool Checked, string Content, Guid? ParentId, Guid MeetingId);