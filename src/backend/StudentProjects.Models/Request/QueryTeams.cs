namespace StudentProjects.Models.Request;

public record QueryTeams(Guid? ProjectId, int Limit, int Offset);