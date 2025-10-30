using System.Text.Json.Serialization;
using StudentProjects.Domain.Enums;

namespace StudentProjects.API.Models.Response;

[JsonConverter(typeof(JsonStringEnumConverter))]
public record UserInfoResponse(
    Guid Id,
    string Email,
    UserRole Role,
    string FirstName,
    string LastName,
    string MiddleName);