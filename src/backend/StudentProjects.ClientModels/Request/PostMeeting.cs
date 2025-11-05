namespace StudentProjects.ClientModels.Request;

public record PostMeeting(Guid TeamId, DateTime Date, string? Name);