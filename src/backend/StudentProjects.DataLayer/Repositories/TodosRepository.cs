using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TodosRepository(DataContext context) : BaseRepository<Todo>(context)
{
    public async Task<List<Todo>> QueryAsync(Guid meetingId)
    {
        return await DataContext.Todos
            .Where(x => x.MeetingId == meetingId)
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task DeleteAsync(Todo entity)
    {
        DataContext.Todos.Remove(entity);
        await DataContext.SaveChangesAsync();
    }
}