using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using StudentProjects.API.Exceptions;
using StudentProjects.Dal;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Services;

public class UserService(DataContext context)
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<List<User>> GetUsersAsync(int offset, int limit)
    {
        return await context.Users.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task PatchUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task<User> GetAuthorizedUserAsync(IEnumerable<Claim> claims)
    {
        var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (userId is null)
            throw new UnauthorizedException("User identifier not specified.");
        var user = await context.Users.FindAsync(Guid.Parse(userId.Value));
        return user ?? throw new UnauthorizedException("User with specified identifier not found.");
    }
}