using StudentProjects.ClientModels.Response;
using User = StudentProjects.ClientModels.Response.User;

namespace StudentProjects.API.Converters;

public static class UserConverter
{
    public static CurrentUser ToInfoResponse(this Domain.Entities.User user)
    {
        return new CurrentUser(
            user.Id,
            user.Email,
            user.Role,
            user.FirstName ?? "",
            user.LastName ?? "",
            user.MiddleName ?? "");
    }

    public static User ToDto(this Domain.Entities.User user)
    {
        return new User(
            user.Id,
            user.FirstName ?? "",
            user.LastName ?? "",
            user.MiddleName ?? "");
    }
}