using StudentProjects.API.Models.Dtos;

namespace StudentProjects.API.Models.Response;

public record ReviewsResponse(ICollection<ReviewDto> Reviews);