using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Request;

namespace StudentProjects.Application.Services;

public class StudentsService(StudentsRepository studentsRepository)
{
    public async Task UpdateTeamStudentsAsync(Guid teamId, PatchStudents request)
    {
        var existingStudents = await studentsRepository.QueryAsync()
    }
}