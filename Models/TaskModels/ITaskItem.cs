using System;
namespace ProgrammingCompetitionService.Models
{
	public interface ITaskItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

