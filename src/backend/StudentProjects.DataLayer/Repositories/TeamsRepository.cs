using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TeamsRepository(DataContext context) : BaseRepository<Team>(context)
{
    public override async Task<Team?> FindTrackedAsync(Guid id)
    {
        return await DataContext.Teams
            .Include(x => x.TeamStudents).ThenInclude(x => x.Student)
            .Include(x => x.TeamStudents).ThenInclude(x => x.Position)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<(List<Team> Data, int Count)> QueryAsync(Guid? projectId, int offset, int limit)
    {
        var query = context.Teams
            .Where(x => projectId == null || x.ProjectId == projectId);

        return (await query.Skip(offset).Take(limit).ToListAsync(), await query.CountAsync());
    }
}