namespace StudentProjects.ClientModels.Response;

public record Review(User Author, bool IsDislike, string Comment);