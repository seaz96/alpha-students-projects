using StudentProjects.DataLayer;
using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using Review = StudentProjects.Domain.Entities.Review;

namespace StudentProjects.Application.Services;

public class ReviewsService(ReviewsRepository reviewsRepository)
{
    public async Task<Models.Response.Review> CreateOrUpdateAsync(Guid caseId, Guid userId, string comment, bool dislike)
    {
        var review = new Review
        {
            CaseId = caseId,
            UserId = userId,
            Comment = comment,
            Dislike = dislike
        };
        await reviewsRepository.AddOrUpdateAsync(review);
        return await GetByIdAsync(caseId, userId);
    }

    public async Task<QueryResponse<Models.Response.Review>> QueryAsync(Guid caseId, CommonQuery query)
    {
        var response = await reviewsRepository.QueryAsync(caseId, query.Offset, query.Limit);
        return new QueryResponse<Models.Response.Review>(response.Data.Select(x => x.ToClientModel()), response.Count);
    }

    private async Task<Models.Response.Review> GetByIdAsync(Guid caseId, Guid userId)
    {
        var review = await reviewsRepository.GetByIdAsync(caseId, userId);
        return review?.ToClientModel() ?? throw new ReviewNotFoundException();
    }
}