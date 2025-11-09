using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;
using StudentProjects.Domain.Enums;

namespace StudentProjects.DataLayer.Repositories;

public class CaseRepository(DataContext context) : BaseRepository<Case>(context)
{
    public async Task<List<Case>> QueryAsync(int offset, int limit, CaseStatus? status)
    {
        return await DataContext.Cases
            .Include(c => c.Author)
            .Where(x => status == null || x.Status == status)
            .Skip(offset)
            .Take(limit).ToListAsync();
    }

    public override async Task<Case?> GetByIdAsync(Guid id)
    {
        return await DataContext.Cases
            .Include(x => x.Reviews)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}