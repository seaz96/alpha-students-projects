using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class TeamsRepository(DataContext context) : BaseRepository<Team>(context);