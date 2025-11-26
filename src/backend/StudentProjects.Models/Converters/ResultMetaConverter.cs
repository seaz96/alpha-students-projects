using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class ResultMetaConverter
{
    public static Models.Response.ResultMeta ToClientModel(this ResultMeta meeting)
    {
        return new Models.Response.ResultMeta(
            meeting.Id,
            meeting.TeamId,
            meeting.Score,
            meeting.Comment);
    }
}