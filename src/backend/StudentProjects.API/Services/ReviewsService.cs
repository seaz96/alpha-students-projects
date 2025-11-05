using StudentProjects.Dal;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Services;

public class ReviewsService(DataContext context)
{
    public async Task<Review> CreateAsync(Guid caseId, Guid userId, string comment, bool dislike)
    {
        var review = new Review
        {
            CaseId = caseId,
            UserId = userId,
            Comment = comment,
            Dislike = dislike
        };
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
        return review;
    }
}