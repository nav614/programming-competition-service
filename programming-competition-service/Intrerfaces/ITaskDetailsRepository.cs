using System;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Intrerfaces
{
	public interface ITaskDetailsRepository: IGenericRepository<TaskDetails>
	{
		Task<TaskDetails> GetById(Guid id);
		Task<TaskDetails> Add(TaskDetails taskDetails);
		Task<TaskDetails> Remove(Guid id);
		Task<IEnumerable<TaskDetails>> GetAll();
		Task<IEnumerable<TaskDetails>> GetByTaskItemId(Guid id);
	}
}

