using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TeamsRepository(DataContext context) : BaseRepository<Team>(context)
{
    public virtual async Task<(List<Team> Data, int Count)> QueryAsync(Guid? projectId, int offset, int limit)
    {
        var query = context.Teams.Where(x => projectId == null || x.ProjectId == projectId);

        return (await query.Skip(offset).Take(limit).ToListAsync(), await query.CountAsync());
    }
}