namespace StudentProjects.Models.Request;

public record QueryStages(Guid? TeamId, int Limit, int Offset);