using System;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Intrerfaces
{
	public interface ICompletedTasksRepository : IGenericRepository<CompletedTaskItem>
	{
		Task<CompletedTaskItem> GetById(Guid id);
		Task<CompletedTaskItem> Add(CompletedTaskItem completedTaskItem);
		Task<CompletedTaskItem> Remove(Guid id);
		Task<IEnumerable<CompletedTaskItem>> GetAll();
	}
}

