using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class ResultMetasRepository(DataContext context) : BaseRepository<ResultMeta>(context)
{
    public virtual async Task<ResultMeta?> FindTrackedByTeamIdAsync(Guid teamId)
    {
        return await DataContext.ResultMetas.FirstOrDefaultAsync(x => x.TeamId == teamId);
    }

    public virtual async Task AddOrUpdateAsync(ResultMeta resultMeta)
    {
        if (DataContext.ResultMetas.Any(x => x.TeamId == resultMeta.TeamId))
            DataContext.Update(resultMeta);
        else
            DataContext.Add(resultMeta);
        await DataContext.SaveChangesAsync();
    }
}