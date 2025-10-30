namespace StudentProjects.API.Models.Request;

public record PatchMeeting(
    string? Name,
    DateTime Date,
    string? Summary,
    string? RecordLink,
    int? Score);