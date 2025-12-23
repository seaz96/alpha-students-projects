using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Entities;
using System.Text;
using StudentProjects.Models.Exceptions;

namespace StudentProjects.API.Controllers;

[Authorize]
[ApiController]
[Route("v1/calendar")]
public class CalendarController(UserService userService, TeamsRepository teamsRepository) : ControllerBase
{
    [HttpGet("{teamId:guid}")]
    public async Task<IActionResult> GetCalendarByTeamAsync(Guid teamId)
    {
        var team = await teamsRepository.FindTrackedAsync(teamId);

        if (team is null)
            throw new TeamNotFoundException();

        var calendarContent = GenerateICalContent(team);

        return File(Encoding.UTF8.GetBytes(calendarContent), "text/calendar", $"team-{team.Id:N}-meetings.icl");
    }

    [HttpGet("mentor-calendar")]
    public async Task<IActionResult> GetMentorCalendarAsync()
    {
        var user = await userService.GetAuthorizedUserAsync();
        if (user == null)
        {
            return Unauthorized();
        }

        var teams = await teamsRepository.GetTeamsByMentorIdAsync(user.Id);
        var calendarContent = GenerateICalContent(teams);

        return File(Encoding.UTF8.GetBytes(calendarContent), "text/calendar", "mentor-calendar.icl");
    }

    [HttpGet("mentor-calendar/{mentorId:guid}")]
    public async Task<IActionResult> GetMentorCalendarByIdAsync(Guid mentorId)
    {
        var user = await userService.GetUserByIdAsync(mentorId);
        if (user == null)
        {
            return Unauthorized();
        }

        var teams = await teamsRepository.GetTeamsByMentorIdAsync(user.Id);
        var calendarContent = GenerateICalContent(teams);

        return File(Encoding.UTF8.GetBytes(calendarContent), "text/calendar", $"user-{mentorId}-meetings.icl");
    }

    private string GenerateICalContent(params IEnumerable<Team> teams)
    {
        var sb = new StringBuilder();
        sb.AppendLine("BEGIN:VCALENDAR");
        sb.AppendLine("VERSION:2.0");
        sb.AppendLine("PRODID:-//StudentProjects//Mentor Calendar//EN");

        foreach (var team in teams)
        {
            foreach (var meeting in team.Meetings)
            {
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine($"SUMMARY:{meeting.Name}");
                sb.AppendLine($"DTSTART:{meeting.Date:yyyyMMddTHHmmssZ}");
                sb.AppendLine($"DESCRIPTION:{meeting.Summary}");
                sb.AppendLine("END:VEVENT");
            }
        }

        sb.AppendLine("END:VCALENDAR");
        return sb.ToString();
    }
}