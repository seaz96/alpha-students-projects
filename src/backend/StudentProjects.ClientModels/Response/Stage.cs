namespace StudentProjects.ClientModels.Response;

public record Stage(
    Guid Id,
    Guid TeamId,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int? UrfuScore,
    int? UrfuComment,
    int? MentorScore,
    int? MentorComment);