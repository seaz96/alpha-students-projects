namespace StudentProjects.Models.Request;

public record QueryStudentPositions(int Offset, int Limit, string? Query);