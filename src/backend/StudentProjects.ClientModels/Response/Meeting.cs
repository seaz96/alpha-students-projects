namespace StudentProjects.ClientModels.Response;

public record Meeting(
    Guid Id,
    string? Name,
    DateTime Date,
    string? Summary,
    string? RecordLink,
    Guid TeamId,
    int? Score,
    Guid? NextId,
    Guid? PreviousId);