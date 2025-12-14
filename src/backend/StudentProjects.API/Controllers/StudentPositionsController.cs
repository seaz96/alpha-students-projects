using Microsoft.AspNetCore.Mvc;
using StudentProjects.Application.Services;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using StudentPosition = StudentProjects.Domain.Entities.StudentPosition;

namespace StudentProjects.API.Controllers;

[Route("/v1/student-positions")]
[ApiController]
public class StudentPositionsController(StudentPositionsService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<QueryResponse<StudentPosition>>> QueryAsync([FromQuery] QueryStudentPositions query)
    {
        return Ok(await service.QueryStudentPositions(query));
    }

    [HttpPost]
    public async Task<ActionResult<StudentPosition>> AddAsync([FromBody] StudentPositionCreate request)
    {
        return Ok(await service.AddStudentPosition(request));
    }

    [HttpPut]
    public async Task<ActionResult<StudentPosition>> UpdateAsync([FromRoute] Guid id, [FromBody] StudentPositionCreate request)
    {
        return Ok(await service.UpdateStudentPosition(id, request.Name));
    }

    [HttpDelete]
    public async Task<ActionResult<StudentPosition>> DeleteAsync([FromRoute] Guid id)
    {
        return Ok(await service.DeleteStudentPosition(id));
    }
}