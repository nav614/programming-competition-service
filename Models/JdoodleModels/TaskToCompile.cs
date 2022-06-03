using System;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingCompetitionService.Models
{
    public class TaskToCompile
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid TaskDetailsId { get; set; }
        [Required]
        public string Script { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string VersionIndex { get; set; }
        [Required]
        public bool IsSumbit { get; set; } = false;
    }
}

