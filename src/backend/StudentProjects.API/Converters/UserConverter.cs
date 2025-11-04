using StudentProjects.API.Models.Dtos;
using StudentProjects.API.Models.Response;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Converters;

public static class UserConverter
{
    public static UserInfoResponse ToInfoResponse(this User user)
    {
        return new UserInfoResponse(
            user.Id,
            user.Email,
            user.Role,
            user.FirstName ?? "",
            user.LastName ?? "",
            user.MiddleName ?? "");
    }

    public static UserDto ToDto(this User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName ?? "",
            user.LastName ?? "",
            user.MiddleName ?? "");
    }
}