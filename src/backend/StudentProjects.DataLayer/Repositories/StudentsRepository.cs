using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class StudentsRepository(DataContext context) : BaseRepository<Student>(context)
{
    public virtual async Task<List<Student>> QueryAsync(int offset, int limit)
    {
        return await DataContext.Students.Skip(offset).Take(limit).ToListAsync();
    }
}