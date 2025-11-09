using StudentProjects.Domain.Enums;

namespace StudentProjects.Models.Request;

public record QueryCases(int Limit, int Offset, CaseStatus? Status);