namespace StudentProjects.Models.Request;

public record GeneratePresignedUrlRequest(Guid TeamId, string Name);