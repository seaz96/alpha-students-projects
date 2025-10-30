namespace StudentProjects.API.Models.Request;

public record QueryMeetings(Guid? TeamId, int Limit, int Offset);