using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TeamsRepository(DataContext context) : BaseRepository<Team>(context)
{
    public virtual async Task<List<Team>> QueryAsync(Guid? projectId, int offset, int limit)
    {
        return await DataContext.Teams
            .Where(x => projectId == null || x.ProjectId == projectId)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
}