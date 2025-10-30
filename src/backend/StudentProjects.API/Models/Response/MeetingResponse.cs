namespace StudentProjects.API.Models.Response;

public record MeetingResponse(
    Guid Id,
    string? Name,
    DateTime Date,
    string? Summary,
    string? RecordLink,
    Guid TeamId,
    int? Score,
    Guid? NextId,
    Guid? PreviousId);