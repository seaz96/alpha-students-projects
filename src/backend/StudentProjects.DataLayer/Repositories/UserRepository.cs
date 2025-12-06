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
}