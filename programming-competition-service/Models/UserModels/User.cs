
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgrammingCompetitionService.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = "user";
        //public List<CompletedTaskItem> CompletedTaskItems { get; set; }
    }
}

