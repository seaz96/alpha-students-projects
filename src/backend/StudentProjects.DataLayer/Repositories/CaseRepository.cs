using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;
using StudentProjects.Domain.Enums;

namespace StudentProjects.DataLayer.Repositories;

public class CaseRepository(DataContext context) : BaseRepository<Case>(context)
{
    public async Task<(List<Case> Data, int Count)> QueryAsync(int offset, int limit, CaseType? type)
    {
        var query = DataContext.Cases
            .Include(c => c.Author)
            .Include(x => x.Reviews)
            .Where(x => type == null || x.Type == type);

        return (await query.Skip(offset).Take(limit).ToListAsync(), await query.CountAsync());
    }

    public override async Task<Case?> FindTrackedAsync(Guid id)
    {
        return await DataContext.Cases
            .Include(x => x.Reviews)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}