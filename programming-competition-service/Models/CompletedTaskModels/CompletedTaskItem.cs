using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingCompetitionService.Models
{
    public class CompletedTaskItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompletedTaskItemId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public TaskItem TaskItem { get; set; }
        public Guid TaskItemId { get; set; }
        public string Language { get; set; }
        public string Script { get; set; }
        public string? Memory { get; set; }
        public string? CpuTime { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
