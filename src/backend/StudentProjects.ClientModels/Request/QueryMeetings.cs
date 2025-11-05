namespace StudentProjects.ClientModels.Request;

public record QueryMeetings(Guid? TeamId, int Limit, int Offset);