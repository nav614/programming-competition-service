using System;
using Microsoft.EntityFrameworkCore;
using ProgrammingCompetitionService.Intrerfaces;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Repositories
{
	public class AuthRepository: GenericRepository<User>, IAuthRepository
	{
		protected readonly PCContext _context;

		public AuthRepository(PCContext context) : base(context)
		{
			_context = context;
		}

        public async Task<User> GetByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
		}
    }
}

