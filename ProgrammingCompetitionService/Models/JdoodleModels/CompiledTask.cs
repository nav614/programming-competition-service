using System;
namespace ProgrammingCompetitionService.Models.JdoodleModels
{
    public class CompiledTask
    {
        public bool IsCompleted { get; set; }
        public string Output { get; set; }
        public int StatusCode { get; set; }
        public string Memory { get; set; }
        public string CpuTime { get; set; }
    }
}

