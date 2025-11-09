using StudentProjects.DataLayer;
using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Entities;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;

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

    public async Task<List<Models.Response.Review>> QueryAsync(Guid caseId, CommonQuery query)
    {
        return (await reviewsRepository.QueryAsync(caseId, query.Offset, query.Limit)).Select(x => x.ToClientModel()).ToList();
    }

    private async Task<Models.Response.Review> GetByIdAsync(Guid caseId, Guid userId)
    {
        var review = await reviewsRepository.GetByIdAsync(caseId, userId);
        return review?.ToClientModel() ?? throw new ReviewNotFoundException();
    }
}