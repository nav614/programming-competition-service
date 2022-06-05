using System;
namespace ProgrammingCompetitionService.Models
{
	public interface IUser
	{
		public string UserName { get; set; }
		public string Role { get; set; }
	}
}

