using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProgrammingCompetitionService.Models
{
	public class UserLogin
	{
		[JsonProperty(PropertyName = "username")]
		[Required]
		public string UserName { get; set; }
		[JsonProperty(PropertyName = "password")]
		[Required]
		public string Password { get; set; }
	}
}

