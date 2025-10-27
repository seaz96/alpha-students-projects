using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Case> Cases { get; set; } = null!;
    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<Meeting> Meetings { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ResultMeta> ResultMetas { get; set; } = null!;
    public DbSet<Stage> Stages { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<TeamStudent> TeamStudents { get; set; } = null!;
    public DbSet<Todo> Todos { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}