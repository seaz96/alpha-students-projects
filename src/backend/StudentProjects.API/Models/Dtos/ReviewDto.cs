namespace StudentProjects.API.Models.Dtos;

public record ReviewDto(UserDto Author, string IsDislike, string Comment);