using System.Text.Json.Serialization;

namespace StudentProjects.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Unknown = 0,
    User = 1,
    Admin = 2,
    SuperAdmin = 3
}