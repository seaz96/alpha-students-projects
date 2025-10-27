using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.API.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.Migrate();
    }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Projects)
            .WithOne(e => e.Author)
            .HasForeignKey(e => e.AuthorId);
        modelBuilder.Entity<Project>()
            .HasMany(e => e.Teams)
            .WithOne(e => e.Project)
            .HasForeignKey(e => e.ProjectId);
        modelBuilder.Entity<TeamStudent>().HasKey(ts => new {ts.TeamId, ts.StudentId});
        modelBuilder.Entity<Like>().HasKey(l => new {l.CaseId, l.UserId});
        modelBuilder.Entity<Team>().HasMany(ts => ts.TeamStudents).WithOne(ts => ts.Team);
        modelBuilder.Entity<Student>().HasMany(ts => ts.TeamStudents).WithOne(ts => ts.Student);
        modelBuilder.Entity<Team>()
            .HasMany(e => e.Meetings)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Likes)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
        modelBuilder.Entity<Case>()
            .HasMany(e => e.Likes)
            .WithOne(e => e.Case)
            .HasForeignKey(e => e.CaseId);
        modelBuilder.Entity<Team>()
            .HasMany(e => e.Stages)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId);
        modelBuilder.Entity<Team>()
            .HasMany(e => e.Stages)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId);
        modelBuilder.Entity<Team>()
            .HasMany(e => e.ResultMetas)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId);
        modelBuilder.Entity<Meeting>()
            .HasMany(e => e.Todos)
            .WithOne(e => e.Meeting)
            .HasForeignKey(e => e.MeetingId);
        modelBuilder.Entity<Meeting>()
            .HasOne(e => e.Next)
            .WithOne(e => e.Previous)
            .HasForeignKey<Meeting>(e => e.NextId);
        modelBuilder.Entity<Meeting>()
            .HasOne(e => e.Previous)
            .WithOne(e => e.Next)
            .HasForeignKey<Meeting>(e => e.PreviousId);
        modelBuilder.Entity<Todo>()
            .HasMany(e => e.Children)
            .WithOne(e => e.Parent)
            .HasForeignKey(e => e.ParentId);
    }
}