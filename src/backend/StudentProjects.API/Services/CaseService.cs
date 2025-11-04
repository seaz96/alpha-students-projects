using Microsoft.EntityFrameworkCore;
using StudentProjects.API.Converters;
using StudentProjects.API.Models.Response;
using StudentProjects.Dal;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Services;

public class CaseService(DataContext context)
{
    public async Task<CaseResponse> AddAsync(string name, string description, User author)
    {
        var entity = new Case
        {
            Id = Guid.NewGuid(),
            AuthorId = author.Id,
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
        await context.Cases.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.ToResponse();
    }

    public async Task<IEnumerable<CaseResponse>> GetAsync(int offset, int limit)
    {
        var cases = await context.Cases
            .Include(x => x.Reviews)
            .OrderBy(x => x.Reviews.Where(r => !r.Dislike))
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return cases.Select(x => x.ToResponse());
    }

    public async Task<CaseResponse?> GetAsync(Guid id)
    {
        var @case = await context.Cases.FindAsync(id);

        return @case?.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.Cases.FindAsync(id);
        if (entity is null)
            return;
        context.Cases.Remove(entity);
        await context.SaveChangesAsync();
    }
}