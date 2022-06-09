using System;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingCompetitionService.Repositories
{
	public class TaskDetailsRepository: GenericRepository<TaskDetails>, ITaskDetailsRepository
	{
		protected readonly PCContext _context;

		public TaskDetailsRepository(PCContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TaskDetails>> GetByTaskItemId(Guid id)
        {
			return await _context.TaskDetails.Where(x => x.TaskItemId == id).ToListAsync();
		}
	}
}

