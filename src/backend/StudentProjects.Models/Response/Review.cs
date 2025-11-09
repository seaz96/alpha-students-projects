namespace StudentProjects.Models.Response;

public record Review(User Author, bool IsDislike, string Comment);