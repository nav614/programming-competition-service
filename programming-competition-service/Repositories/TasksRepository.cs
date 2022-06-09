using System;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingCompetitionService.Repositories
{
    public class TasksRepository : GenericRepository<TaskItem>, ITasksRepository
    {
        public TasksRepository(PCContext context) : base(context)
        {
        }
    }
}

