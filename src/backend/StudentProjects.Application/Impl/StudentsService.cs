using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Entities;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Request;

namespace StudentProjects.Application.Services;

//note: храним в таблице данные, как отдельно список студентов и отдельно таблица со связями студента с конкретной командой
// так сделано, чтобы в будущем можно было добавить в модель студента необходимые поля и создавать для студентов аккаунты
// и они могли бы хранить там данные
// в противном случае можно будет легко переделать на модель, где студент это лишь запись к определенной команде
public class StudentsService(StudentsRepository studentsRepository, TeamsStudentsRepository teamStudentRepository)
{
    public async Task<ICollection<Models.Response.TeamStudent>> AddTeamStudentsAsync(Guid teamId, PostStudents request)
    {
        var result = new List<Models.Response.TeamStudent>();
        foreach (var student in request.Students)
        {
            var studentEntity = new Student
            {
                Id = Guid.NewGuid(),
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                Phone = student.Phone,
                Telegram = student.Telegram
            };
            await studentsRepository.AddAsync(studentEntity);
            var teamStudent = new TeamStudent
            {
                TeamId = teamId,
                StudentId = studentEntity.Id,
                AcademicGroup = student.AcademicGroup,
                PositionId = student.PositionId,
                Student = studentEntity
            };
            await teamStudentRepository.AddAsync(teamStudent);
            result.Add(teamStudent.ToClientModel());
        }
        return result;
    }

    public async Task<IEnumerable<Models.Response.TeamStudent>> UpdateTeamStudentsAsync(Guid teamId, PatchStudents request)
    {
        var entities = await teamStudentRepository.QueryByTeamIdAsync(teamId);
        foreach (var entity in entities)
        {
            var patch = request.Students.FirstOrDefault(x => x.Id == entity.StudentId);
            if (patch is null)
                continue;
            entity.PositionId = patch.PositionId ?? entity.PositionId;
            entity.AcademicGroup = patch.AcademicGroup ?? entity.AcademicGroup;
            entity.Student.FirstName = patch.FirstName ?? entity.Student.FirstName;
            entity.Student.MiddleName = patch.MiddleName ?? entity.Student.MiddleName;
            entity.Student.LastName = patch.LastName ?? entity.Student.LastName;
            entity.Student.Phone = patch.Phone ?? entity.Student.Phone;
            entity.Student.Email = patch.Email ?? entity.Student.Email;
            entity.Student.Telegram = patch.Telegram ?? entity.Student.Telegram;
            await teamStudentRepository.UpdateAsync(entity);
        }

        return entities.Select(x => x.ToClientModel());
    }

    public async Task<IEnumerable<Models.Response.TeamStudent>> DeleteTeamStudentsAsync(Guid teamId, DeleteStudents request)
    {
        foreach (var studentId in request.Students)
        {
            await teamStudentRepository.DeleteByIdAsync(teamId, studentId);
        }
        return (await teamStudentRepository.QueryByTeamIdAsync(teamId)).Select(x => x.ToClientModel());
    }
}