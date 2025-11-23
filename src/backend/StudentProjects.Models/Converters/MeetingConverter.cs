using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class MeetingConverter
{
    public static Models.Response.Meeting ToClientModel(this Meeting meeting)
    {
        return new Models.Response.Meeting(
            meeting.Id,
            meeting.Name,
            meeting.Date,
            meeting.Summary,
            meeting.RecordLink,
            meeting.TeamId,
            meeting.Score,
            meeting.NextId,
            meeting.PreviousId);
    }
}