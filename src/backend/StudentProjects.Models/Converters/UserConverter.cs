using StudentProjects.Models.Response;
using User = StudentProjects.Models.Response.User;

namespace StudentProjects.Models.Converters;

public static class UserConverter
{
    public static UserAccount ToAccountResponse(this Domain.Entities.User user)
    {
        return new UserAccount(
            user.Id,
            user.Email,
            user.Role,
            user.FirstName ?? "",
            user.MiddleName ?? "",
            user.LastName ?? "");
    }

    public static User ToDto(this Domain.Entities.User user)
    {
        return new User(
            user.Id,
            user.FirstName ?? "",
            user.MiddleName ?? "",
            user.LastName ?? "");
    }
}