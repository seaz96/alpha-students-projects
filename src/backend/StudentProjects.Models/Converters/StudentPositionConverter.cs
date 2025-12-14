using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class StudentPositionConverter
{
    public static Models.Response.StudentPosition ToClientModel(this StudentPosition position)
    {
        return new Response.StudentPosition(position.Id, position.Name);
    }
}