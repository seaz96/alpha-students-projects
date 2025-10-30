namespace StudentProjects.API.Models.Response;

public record TeamResponse(Guid Id, string Name, string Description, string TeamprojectLink, Guid ProjectId);