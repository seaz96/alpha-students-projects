using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class BaseRepository<T>(DataContext context) where T : BaseEntity
{
    protected readonly DataContext DataContext = context;

    public virtual async Task<T?> FindTrackedAsync(Guid id)
    {
        return await DataContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<List<T>> QueryAsync(int offset, int limit)
    {
        return await DataContext.Set<T>().Skip(offset).Take(limit).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DataContext.Set<T>().AddAsync(entity);
        await DataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        DataContext.Set<T>().Update(entity);
        await DataContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        DataContext.Set<T>().Remove(entity);
        await DataContext.SaveChangesAsync();
    }
}