using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<Project>(context)
{
    public override async Task<Project?> FindTrackedAsync(Guid id)
    {
        return await DataContext.Projects
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<List<Project>> QueryAsync(int offset, int limit)
    {
        return await DataContext.Projects.Include(x => x.Author).Skip(offset).Take(limit).ToListAsync();
    }
}