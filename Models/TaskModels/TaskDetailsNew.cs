using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgrammingCompetitionService.Models
{
	public class TaskDetailsNew: ITaskDetails
	{
        [Required]
        public Guid TaskItemId { get; set; }
        [Required]
        public string MainScript { get; set; }
        [Required]
        public string Output { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string UserScript { get; set; }
        [Required]
        public string Input { get; set; }
    }
}

