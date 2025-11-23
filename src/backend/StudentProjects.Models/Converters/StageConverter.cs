using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class StageConverter
{
    public static Models.Response.Stage ToClientModel(this Stage stage)
    {
        return new Models.Response.Stage(
            stage.Id,
            stage.TeamId,
            stage.Name ?? string.Empty,
            stage.StartDate,
            stage.EndDate,
            stage.UrfuScore,
            stage.UrfuComment,
            stage.MentorScore,
            stage.MentorComment
        );
    }
}