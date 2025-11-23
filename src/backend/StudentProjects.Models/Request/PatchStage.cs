namespace StudentProjects.Models.Request;

public record PatchStage(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int? UrfuScore,
    string? UrfuComment,
    int? MentorScore,
    string? MentorComment);