namespace StudentProjects.Models.Request;

public record QueryMeetings(Guid? TeamId, int Limit, int Offset);