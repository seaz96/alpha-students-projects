namespace StudentProjects.ClientModels.Request;

public record PostStage(Guid TeamId, string? Name, DateTime StartDate, DateTime EndDate);