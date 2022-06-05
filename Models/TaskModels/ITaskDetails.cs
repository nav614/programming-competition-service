using System;

namespace ProgrammingCompetitionService.Models
{
    public class ITaskDetails
    {
        public Guid TaskItemId { get; set; }
        public string MainScript { get; set; }
        public string Output { get; set; }
        public string Language { get; set; }
        public string UserScript { get; set; }
        public string Input { get; set; }
    }
}


