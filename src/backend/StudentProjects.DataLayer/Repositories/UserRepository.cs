using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class UserRepository(DataContext context) : BaseRepository<User>(context)
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return  DataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}