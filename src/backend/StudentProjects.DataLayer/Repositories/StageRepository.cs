using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class StageRepository(DataContext ctx) : BaseRepository<Stage>(ctx)
{
    public async Task<(List<Stage> Data, int Count)> QueryAsync(Guid? teamId, int offset, int limit)
    {
        var query = DataContext.Stages
            .Where(x => teamId == null || x.TeamId == teamId)
            .AsNoTracking();

        return (await query.Skip(offset).Take(limit).ToListAsync(), await query.CountAsync());
    }
}