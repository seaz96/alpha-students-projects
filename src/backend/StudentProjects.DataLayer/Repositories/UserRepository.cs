using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class UserRepository(DataContext context) : BaseRepository<User>(context)
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return DataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public virtual async Task<List<User>> GetBatchByIdAsync(IEnumerable<Guid> ids)
    {
        return await DataContext.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public virtual async Task<(List<User> Data, int Count)> QueryAsync(int offset, int limit, string? query)
    {
        var count = await DataContext.Users.ApplyQuery(query).CountAsync();
        var data = await DataContext.Users
            .ApplyQuery(query)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
        return (data, count);
    }
}

//todo: перекинуть и перевести на нормальный поиск
internal static class QueryableExtensions
{
    public static IQueryable<User> ApplyQuery(this IQueryable<User> queryable, string? query)
    {
        if (query is null)
            return queryable;
        var queryText = query.ToLower();
        return queryable.Where(x => 
            x.Email.ToLower().Contains(queryText)
            || (x.FirstName ?? "").ToLower().Contains(queryText)
            || (x.MiddleName ?? "").ToLower().Contains(queryText)
            || (x.LastName ?? "").ToLower().Contains(queryText)
            || x.Id.ToString().StartsWith(queryText));
    }
}