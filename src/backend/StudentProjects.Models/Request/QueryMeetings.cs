namespace StudentProjects.Models.Request;

public record QueryMeetings(Guid? TeamId, DateTime StartDate, DateTime EndDate);