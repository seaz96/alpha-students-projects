using StudentProjects.Domain.Entities;
using User = StudentProjects.Models.Response.User;

namespace StudentProjects.Models.Converters;

public static class ReviewConverter
{
    public static Models.Response.Review ToClientModel(this Review review)
    {
        return new Models.Response.Review(review.User.ToDto(), review.Dislike, review.Comment ?? "");
    }
}