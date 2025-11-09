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
}