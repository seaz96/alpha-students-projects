using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class MeetingRepository(DataContext context) : BaseRepository<Meeting>(context)
{
    public async Task<(List<Meeting> Data, int Count)> QueryAsync(Guid? teamId, DateTime StartDate, DateTime EndDate)
    {
        var query = DataContext.Meetings
            .Where(x => 
                (teamId == null || x.TeamId == teamId)
                && x.Date >= StartDate
                && x.Date <= EndDate)
            .AsNoTracking();
        
        return (await query.ToListAsync(), await query.CountAsync());
    }
}