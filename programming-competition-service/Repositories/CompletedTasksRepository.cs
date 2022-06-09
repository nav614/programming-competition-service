using System;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;


namespace ProgrammingCompetitionService.Repositories
{
	public class CompletedTasksRepository: GenericRepository<CompletedTaskItem>, ICompletedTasksRepository
	{
		public CompletedTasksRepository(PCContext context) : base(context)
		{
		}
	}
}

