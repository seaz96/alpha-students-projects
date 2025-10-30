namespace StudentProjects.API.Models.Request;

public record QueryTeams(Guid? ProjectId, int Limit, int Offset);