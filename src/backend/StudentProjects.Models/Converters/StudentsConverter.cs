using StudentProjects.Models.Response;

namespace StudentProjects.Models.Converters;

public static class StudentsConverter
{
    public static TeamStudent ToClientModel(this Domain.Entities.TeamStudent x)
    {
        return new TeamStudent(
            x.StudentId,
            x.Student.FirstName ?? "",
            x.Student.MiddleName ?? "",
            x.Student.LastName ?? "",
            x.Student.Phone ?? "",
            x.Student.Email ?? "",
            x.Student.Telegram ?? "",
            x.Position?.Name ?? "",
            x.AcademicGroup ?? "");
    }
}