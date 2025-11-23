using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class TodosConverter
{
    public static Models.Response.Todo ToClientModel(this Todo todo)
    {
        return new Models.Response.Todo(
            todo.Id,
            todo.Checked,
            todo.Content ?? "",
            todo.ParentId,
            todo.MeetingId);
    }
}