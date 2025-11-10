using Microsoft.EntityFrameworkCore;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class StudentsRepository(DataContext context) : BaseRepository<Team>(context);