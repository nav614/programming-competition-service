using System;
using Newtonsoft.Json;

namespace ProgrammingCompetitionService.Models
{
	public class JdoodleOutput
	{
        [JsonProperty(PropertyName = "output")]
        public string Output { get; set; }
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty(PropertyName = "memory")]
        public string Memory { get; set; }
        [JsonProperty(PropertyName = "cpuTime")]
        public string CpuTime { get; set; }
    }
}

