using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;

namespace StudentProjects.Application.Services;

public class StudentPositionsService(StudentPositionsRepository repository)
{
    public async Task<QueryResponse<StudentPosition>> QueryStudentPositions(QueryStudentPositions request)
    {
        var response = await repository.QueryAsync(request.Offset, request.Limit, request.Query);
        return new QueryResponse<StudentPosition>(response.Data.Select(x => x.ToClientModel()), response.Count);
    }

    public async Task<StudentPosition> AddStudentPosition(StudentPositionCreate request)
    {
        var position = new Domain.Entities.StudentPosition
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        await repository.AddAsync(position);

        return position.ToClientModel();
    }

    public async Task<StudentPosition> UpdateStudentPosition(Guid id, string name)
    {
        var position = await repository.FindTrackedAsync(id);
        if (position == null)
            throw new StudentPositionNotFoundException();
        position.Name = name;
        await repository.UpdateAsync(position);

        return position.ToClientModel();
    }

    public async Task<StudentPosition> DeleteStudentPosition(Guid id)
    {
        var position = await repository.FindTrackedAsync(id);
        if (position is null)
            throw new StudentPositionNotFoundException();
        await repository.DeleteAsync(position);

        return position.ToClientModel();
    }
}