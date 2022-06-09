using System;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Intrerfaces
{
	public interface ITasksRepository: IGenericRepository<TaskItem>
	{
		Task<TaskItem> GetById(Guid id);
		Task<TaskItem> Add(TaskItem taskItem);
		Task<TaskItem> Remove(Guid id);
		Task<IEnumerable<TaskItem>> GetAll();
	}
}

