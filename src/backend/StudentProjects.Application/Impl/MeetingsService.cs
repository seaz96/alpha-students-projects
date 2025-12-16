using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using Meeting = StudentProjects.Domain.Entities.Meeting;

namespace StudentProjects.Application.Services;

public class MeetingsService(MeetingRepository meetingRepository)
{
    public async Task<Models.Response.Meeting> CreateAsync(PostMeeting request)
    {
        var meeting = new Meeting
        {
            Id = Guid.NewGuid(),
            Date = request.Date,
            TeamId = request.TeamId,
            Name = request.Name
        };
        await meetingRepository.AddAsync(meeting);
        //todo(azanov.n): добавить вычисление предыдущей и следующей по списку встречи
        return meeting.ToClientModel();
    }

    public async Task<Models.Response.Meeting> UpdateAsync(Guid id, PatchMeeting request)
    {
        var meeting = await meetingRepository.FindTrackedAsync(id);
        if (meeting is null)
            throw new MeetingNotFoundException();
        meeting.Name = request.Name;
        meeting.Date = request.Date;
        meeting.Summary = request.Summary;
        meeting.RecordLink = request.RecordLink;
        meeting.Score = request.Score;
        await meetingRepository.UpdateAsync(meeting);
        return meeting.ToClientModel();
    }

    public async Task<QueryResponse<Models.Response.Meeting>> QueryAsync(QueryMeetings request)
    {
        var response = await meetingRepository.QueryAsync(request.TeamId, request.StartDate, request.EndDate);
        return new QueryResponse<Models.Response.Meeting>(response.Data.Select(x => x.ToClientModel()), response.Count);
    }

    public async Task<Models.Response.Meeting> GetAsync(Guid id)
    {
        return (await meetingRepository.FindTrackedAsync(id) ?? throw new MeetingNotFoundException()).ToClientModel();
    }

    public async Task<Models.Response.Meeting> DeleteAsync(Guid id)
    {
        var meeting = await meetingRepository.FindTrackedAsync(id) ?? throw new MeetingNotFoundException();
        await  meetingRepository.DeleteAsync(meeting);
        return meeting.ToClientModel();
    }
}