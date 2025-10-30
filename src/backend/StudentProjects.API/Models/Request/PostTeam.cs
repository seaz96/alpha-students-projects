namespace StudentProjects.API.Models.Request;

public record PostTeam(Guid ProjectId, string? Name, string? TeamprojectLink);