using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;
using StudentProjects.Domain.Enums;

namespace StudentProjects.DataLayer.Repositories;

public class CaseRepository(DataContext context) : BaseRepository<Case>(context)
{
    public async Task<List<Case>> QueryAsync(int offset, int limit, CaseType? type)
    {
        return await DataContext.Cases
            .Include(c => c.Author)
            .Include(x => x.Reviews)
            .Where(x => type == null || x.Type == type)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public override async Task<Case?> GetByIdAsync(Guid id)
    {
        return await DataContext.Cases
            .Include(x => x.Reviews)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}