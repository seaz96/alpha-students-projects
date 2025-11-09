using System.Text.Json.Serialization;

namespace StudentProjects.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseStatus
{
    Unknown = 0,
    Request = 1,
    Submitted = 2
}