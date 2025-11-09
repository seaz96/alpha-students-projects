namespace StudentProjects.Models.Request;

public record PostMeeting(Guid TeamId, DateTime Date, string? Name);