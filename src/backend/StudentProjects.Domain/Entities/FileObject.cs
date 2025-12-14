namespace StudentProjects.Domain.Entities;

public class FileObject
{
    public bool IsDirectory { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public ulong Size { get; set; }
}