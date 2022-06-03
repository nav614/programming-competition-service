using System;
using Newtonsoft.Json;

namespace ProgrammingCompetitionService.Models
{
    public class JdoodleInput
    {
        [JsonProperty(PropertyName = "сlientId")]
        public string ClientId { get; set; }
        [JsonProperty(PropertyName = "сlientSecret")]
        public string ClientSecret { get; set; }
        [JsonProperty(PropertyName = "script")]
        public string Script { get; set; }
        [JsonProperty(PropertyName = "stdin")]
        public string Stdin { get; set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
        [JsonProperty(PropertyName = "versionIndex")]
        public string VersionIndex { get; set; }
    }
}

