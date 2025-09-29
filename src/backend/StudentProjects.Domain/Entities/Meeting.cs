namespace StudentProjects.Domain.Entities;

public class Meeting
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    //todo: нужно ли?
    public string Name { get; set; }
    public string Description { get; set; }
    public string Record { get; set; }
    public string Summary { get; set; }
}