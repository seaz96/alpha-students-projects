using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class MeetingRepository(DataContext context) : BaseRepository<Meeting>(context)
{
    public async Task<List<Meeting>> QueryAsync(Guid? teamId, int offset, int limit)
    {
        return await DataContext.Meetings
            .Where(x => teamId == null || x.TeamId == teamId)
            .Skip(offset)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync();
    }
}