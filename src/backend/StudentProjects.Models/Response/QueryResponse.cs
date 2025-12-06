namespace StudentProjects.Models.Response;

public class QueryResponse<T>(IEnumerable<T> data, int total)
{
    public IEnumerable<T> Data { get; set; } = data;
    public int Total { get; set; } = total;
}