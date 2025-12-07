namespace StudentProjects.Models.Request;

public record QueryUserAccounts(int Limit, int Offset, string? Query);