using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Converters;

public static class ReviewConverter
{
    public static ClientModels.Response.Review ToClientModel(this Review review, ClientModels.Response.User author)
    {
        return new ClientModels.Response.Review(author, review.Dislike, review.Comment ?? "");
    }
}