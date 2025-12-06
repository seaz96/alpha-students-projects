using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class ReviewsRepository(DataContext context)
{
    public async Task AddOrUpdateAsync(Review review)
    {
        var entity = await GetByIdAsync(review.CaseId, review.UserId);
        if (entity is null)
        {
            await context.Reviews.AddAsync(review);
        }
        else
        {
            entity.Comment = review.Comment;
            entity.Dislike = review.Dislike;
        }
        await context.SaveChangesAsync();
    }

    public async Task<Review?> GetByIdAsync(Guid caseId, Guid userId)
    {
        return await context.Reviews.FindAsync(caseId, userId);
    }

    public async Task<(List<Review> Data, int Count)> QueryAsync(Guid caseId, int offset, int limit)
    {
       var query = context.Reviews
           .Include(x => x.Case)
           .Include(x => x.User)
           .Where(x => x.CaseId == caseId)
           .AsNoTracking();

        return (await query.Skip(offset).Take(limit).ToListAsync(), await query.CountAsync());
    }
}