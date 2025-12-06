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

    public override async Task<(List<Project> Data, int Count)> QueryAsync(int offset, int limit)
    {
        var query = DataContext.Projects
            .Include(x => x.Author)
            .Include(x => x.Mentors);
        
        return (await query.Skip(offset).Take(limit).ToListAsync(), await query.CountAsync());
    }

    public async Task UpdateMentorsAsync(Project project)
    {
        var mentors = context.Users.Where(x => project.Mentors.Select(m => m.Id).Contains(x.Id)).ToList();
        project.Mentors = mentors;
        await DataContext.SaveChangesAsync();
    }
}