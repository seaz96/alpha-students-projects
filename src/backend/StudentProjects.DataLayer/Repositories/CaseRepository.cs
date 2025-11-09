using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class CaseRepository(DataContext context) : BaseRepository<Case>(context)
{
    public override async Task<List<Case>> QueryAsync(int offset, int limit)
    {
        return await DataContext.Cases.Include(c => c.Author).Skip(offset).Take(limit).ToListAsync();
    }

    public override async Task<Case?> GetByIdAsync(Guid id)
    {
        return await DataContext.Cases
            .Include(x => x.Reviews)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}