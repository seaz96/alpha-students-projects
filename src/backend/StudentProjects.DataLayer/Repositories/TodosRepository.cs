using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TodosRepository(DataContext context) : BaseRepository<Todo>(context)
{
    public async Task<(List<Todo> Data, int Count)> QueryAsync(Guid meetingId)
    {
        var query = context.Todos
            .Where(x => x.MeetingId == meetingId)
            .AsNoTracking();

        return (await query.ToListAsync(), await query.CountAsync());
    }

    public override async Task DeleteAsync(Todo entity)
    {
        DataContext.Todos.Remove(entity);
        await DataContext.SaveChangesAsync();
    }
}