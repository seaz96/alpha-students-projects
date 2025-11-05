using System.ComponentModel.DataAnnotations;

namespace StudentProjects.ClientModels.Request;

public record RegisterUser([EmailAddress] string Email, [MinLength(8)] string Password);