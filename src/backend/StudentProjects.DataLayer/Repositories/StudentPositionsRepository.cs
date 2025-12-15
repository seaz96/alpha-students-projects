using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class StudentPositionsRepository(DataContext context) : BaseRepository<StudentPosition>(context)
{
    public async Task<(List<StudentPosition> Data, int Count)> QueryAsync(int offset, int limit, string? query)
    {
        var request = context.StudentPositions
            .Where(x => x.Name.ToLower().Contains((query ?? "").ToLower()))
            .AsNoTracking();

        return (await request.Skip(offset).Take(limit).ToListAsync(), await request.CountAsync());
    }
}