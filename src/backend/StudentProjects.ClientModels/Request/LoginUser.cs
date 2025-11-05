using System.ComponentModel.DataAnnotations;

namespace StudentProjects.ClientModels.Request;

public record LoginUser([EmailAddress] string Email, [MinLength(8)] string Password);