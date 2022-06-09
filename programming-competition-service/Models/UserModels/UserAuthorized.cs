using System;
using Newtonsoft.Json;

namespace ProgrammingCompetitionService.Models
{
	public class UserAuthorized
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Token { get; set; }
		public string Role { get; set; }

	}
}

