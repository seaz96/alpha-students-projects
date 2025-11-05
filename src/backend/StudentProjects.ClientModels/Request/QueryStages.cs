namespace StudentProjects.ClientModels.Request;

public record QueryStages(Guid? TeamId, int Limit, int Offset);