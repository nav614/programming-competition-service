using Microsoft.EntityFrameworkCore;

namespace ProgrammingCompetitionService.Models
{
    public class PCContext : DbContext
    {
        public PCContext(DbContextOptions<PCContext> options)
    : base(options)
        { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CompletedTaskItem> CompletedTasks { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=Data/PCdatabase.db");
    }
}

