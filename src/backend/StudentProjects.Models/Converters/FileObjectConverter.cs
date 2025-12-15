using StudentProjects.Domain.Entities;

namespace StudentProjects.Models.Converters;

public static class FileObjectConverter
{
    public static Models.Response.FileObject ToClientModel(this FileObject file)
    {
        return new Models.Response.FileObject(file.Id, file.TeamId, file.Name, file.Size);
    }
}