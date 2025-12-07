using System.Text.Json.Serialization;

namespace StudentProjects.Models.Response;

public record Team(
    Guid Id,
    string Name,
    string Description,
    string TeamprojectLink,
    Guid ProjectId,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] IEnumerable<TeamStudent>? Students);