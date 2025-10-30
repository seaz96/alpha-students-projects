namespace StudentProjects.API.Models.Request;

public record PatchUserRequest(string? FirstName, string? LastName, string? MiddleName);