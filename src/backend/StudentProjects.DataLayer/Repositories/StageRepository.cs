using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class StageRepository(DataContext ctx) : BaseRepository<Stage>(ctx)
{
    public async Task<List<Stage>> QueryAsync(Guid? teamId, int offset, int limit)
    {
        return await DataContext.Stages
            .Where(x => teamId == null || x.TeamId == teamId)
            .Skip(offset)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync();
    }
}