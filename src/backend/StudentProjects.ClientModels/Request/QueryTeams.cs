namespace StudentProjects.ClientModels.Request;

public record QueryTeams(Guid? ProjectId, int Limit, int Offset);