using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TeamsStudentsRepository(DataContext context)
{
    public async Task AddAsync(TeamStudent entity)
    {
        await context.TeamStudents.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<TeamStudent>> QueryByTeamIdAsync(Guid teamId)
    {
        return await context.TeamStudents
            .Include(x => x.Student)
            .Where(x => x.TeamId == teamId)
            .ToListAsync();
    }

    public async Task UpdateAsync(TeamStudent entity)
    {
        context.TeamStudents.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid teamId, Guid studentId)
    {
        await context.TeamStudents.Where(x => x.StudentId == studentId && x.TeamId == teamId).ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}