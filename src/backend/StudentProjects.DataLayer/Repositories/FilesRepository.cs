using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class FilesRepository(DataContext context) : BaseRepository<FileObject>(context)
{
    public async Task<List<FileObject>> GetFilesByTeamAsync(Guid teamId)
    {
        return await context.Files.Where(x => x.TeamId == teamId).AsNoTracking().ToListAsync();
    }
}