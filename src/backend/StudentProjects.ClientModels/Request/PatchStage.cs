namespace StudentProjects.ClientModels.Request;

public record PatchStage(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int? UrfuScore,
    int? UrfuComment,
    int? MentorScore,
    int? MentorComment);