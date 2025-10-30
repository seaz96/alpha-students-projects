namespace StudentProjects.API.Models.Response;

public record StageResponse(
    Guid Id,
    Guid TeamId,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int? UrfuScore,
    int? UrfuComment,
    int? MentorScore,
    int? MentorComment);