namespace StudentProjects.Models.Response;

public record Stage(
    Guid Id,
    Guid TeamId,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int? UrfuScore,
    string? UrfuComment,
    int? MentorScore,
    string? MentorComment);