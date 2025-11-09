using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Enums;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using User = StudentProjects.Domain.Entities.User;

namespace StudentProjects.Application.Services;

public class UserService(UserRepository userRepository, IHttpContextAccessor contextAccessor)
{
    public async Task<UserAccount> LoginUserAsync(LoginUser request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            throw new UnauthorizedException();

        var passwordVerified = PasswordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);
        return !passwordVerified
            ? throw new UnauthorizedException()
            : user.ToAccountResponse();
    }

    public async Task<UserAccount> RegisterAsync(RegisterUser request)
    {
        if (await userRepository.GetByEmailAsync(request.Email) is not null)
            throw new EmailRegisteredException();

        var passwordHash = PasswordHasher.HashPassword(request.Password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = passwordHash
        };

        await userRepository.AddAsync(user);
        return user.ToAccountResponse();
    }

    public async Task<UserAccount> UpdateUserInfoAsync(PatchUser patch)
    {
        var user = await GetAuthorizedUserAsync();
        user.FirstName = patch.FirstName;
        user.LastName = patch.LastName;
        user.MiddleName = patch.MiddleName;
        await userRepository.UpdateAsync(user);
        return user.ToAccountResponse();
    }

    public async Task<UserAccount> UpdateUserRoleAsync(Guid userId, UserRole role)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new UserNotFoundException();
        user.Role = role;
        await userRepository.UpdateAsync(user);
        return user.ToAccountResponse();
    }

    public async Task<List<UserAccount>> QueryUserAccountsAsync(CommonQuery request)
    {
        var users = await userRepository.QueryAsync(request.Offset, request.Limit);
        return users.Select(x => x.ToAccountResponse()).ToList();
    }

    public async Task<UserAccount> GetAuthorizedUserAccountAsync()
    {
        return (await GetAuthorizedUserAsync()).ToAccountResponse();
    }

    public async Task<StudentProjects.Models.Response.User> GetAuthorizedUserDtoAsync()
    {
        return (await GetAuthorizedUserAsync()).ToDto();
    }

    private async Task<User> GetAuthorizedUserAsync()
    {
        if (!Guid.TryParse(contextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var userId))
            throw new UnauthorizedException("User identifier not specified.");
        var user = await userRepository.GetByIdAsync(userId);
        return user ?? throw new UnauthorizedException("User with specified identifier not found.");
    }
}