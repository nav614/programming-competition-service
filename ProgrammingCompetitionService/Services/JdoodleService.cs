using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ProgrammingCompetitionService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ProgrammingCompetitionService.Services
{
    public class JdoodleService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public JdoodleService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<JdoodleOutput> Execute(string script, string language, string versionIndex)
        {
            JdoodleInput jdoodleInput = new JdoodleInput()
            {
                ClientId = _configuration.GetSection("AppSettings:Jdoodle:ClientId").Value,
                ClientSecret = _configuration.GetSection("AppSettings:Jdoodle:ClientSecret").Value,
                Script = script,
                Language = language,
                VersionIndex = versionIndex
            };

            using var response = await _httpClient.PostAsJsonAsync("v1/execute", jdoodleInput); // PostAsync("v1/execute", jdoodleInputJson);

            return await response.Content.ReadFromJsonAsync<JdoodleOutput>();
        }
    }
}
