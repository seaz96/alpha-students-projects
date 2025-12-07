using StudentProjects.DataLayer.Repositories;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;
using StudentProjects.Models.Request;
using StudentProjects.Models.Response;
using Todo = StudentProjects.Domain.Entities.Todo;

namespace StudentProjects.Application.Services;

public class TodoService(TodosRepository todosRepository)
{
    public async Task<Models.Response.Todo> CreateAsync(PostTodo request)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Checked = false,
            Content = request.Content,
            MeetingId = request.MeetingId,
            ParentId = request.ParentId,
        };

        await todosRepository.AddAsync(todo);
        return todo.ToClientModel();
    }

    public async Task<Models.Response.Todo> UpdateAsync(Guid id, PatchTodo request)
    {
        var todo = await todosRepository.FindTrackedAsync(id);
        if (todo is null)
            throw new TodoNotFoundException();
        todo.Content = request.Content ?? todo.Content;
        todo.Checked = request.Checked ?? todo.Checked;
        await todosRepository.UpdateAsync(todo);
        return todo.ToClientModel();
    }

    public async Task<Models.Response.Todo> DeleteAsync(Guid todoId)
    {
        var todo = await todosRepository.FindTrackedAsync(todoId);
        if (todo is null)
            throw new TodoNotFoundException();
        await todosRepository.DeleteAsync(todo);
        return todo.ToClientModel();
    }

    public async Task<Models.Response.Todo> GetByIdAsync(Guid todoId)
    {
        return (await todosRepository.FindTrackedAsync(todoId) ?? throw new TodoNotFoundException()).ToClientModel();
    }

    public async Task<QueryResponse<Models.Response.Todo>> QueryAsync(Guid meetingId)
    {
        var response = await todosRepository.QueryAsync(meetingId);
        return new QueryResponse<Models.Response.Todo>(response.Data.Select(x => x.ToClientModel()), response.Count);
    }
}