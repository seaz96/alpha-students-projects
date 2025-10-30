using System.Text.Json.Serialization;

namespace StudentProjects.Domain.Enums;

//todo: check later
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProjectStatus
{
    Unknown = 0,
    Active = 1,
    Archived = 2
}