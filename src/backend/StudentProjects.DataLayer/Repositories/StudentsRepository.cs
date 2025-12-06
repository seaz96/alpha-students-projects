using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class StudentsRepository(DataContext context) : BaseRepository<Student>(context)
{
    public override async Task<(List<Student> Data, int Count)> QueryAsync(int offset, int limit)
    {
        return (await DataContext.Students.Skip(offset).Take(limit).ToListAsync(), await DataContext.Students.CountAsync());
    }
}