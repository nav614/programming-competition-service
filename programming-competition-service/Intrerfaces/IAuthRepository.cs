using System;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Intrerfaces
{
	public interface IAuthRepository: IGenericRepository<User>
	{
		Task<User> GetByUserName(string userName);
		Task<User> Add(User entity);
	}
}

