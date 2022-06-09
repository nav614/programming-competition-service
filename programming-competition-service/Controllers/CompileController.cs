using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;
using ProgrammingCompetitionService.Models.JdoodleModels;
using ProgrammingCompetitionService.Services;
using ProgrammingCompetitionService.Utils;

namespace ProgrammingCompetitionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompileController : ControllerBase
    {
        private readonly ITaskDetailsRepository _taskDetailsRepository;
        private readonly ICompletedTasksRepository _completedTasksRepository;
        private readonly IConfiguration _configuration;
        private readonly JdoodleService _jdoodleService;


        public CompileController(ITaskDetailsRepository taskDetailsRepository, ICompletedTasksRepository completedTasksRepository, IConfiguration configuration, JdoodleService jdoodleService)
        {
            _taskDetailsRepository = taskDetailsRepository;
            _completedTasksRepository = completedTasksRepository;
            _configuration = configuration;
            _jdoodleService = jdoodleService;
        }

        [HttpPost("run")]
        public async Task<ActionResult<CompiledTask>> Run(TaskToCompile taskToCompile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var taskDetails = await _taskDetailsRepository.GetById(taskToCompile.TaskDetailsId);
            if (taskDetails == null)
            {
                return BadRequest("Task details not found.");
            }

            var userScriptKeyword = _configuration.GetSection("AppSettings:Consts:UserScript").Value;
            var script = taskDetails.MainScript.Replace(userScriptKeyword, taskToCompile.Script);
            var jdoodleOutput = new JdoodleOutput();

            try
            {
                jdoodleOutput = await _jdoodleService.Execute(script, taskToCompile.Language, taskToCompile.VersionIndex); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (jdoodleOutput.StatusCode != 200)
            {
                return StatusCode(jdoodleOutput.StatusCode);
            }

            var isCompleted = TaskVerification.IsCompleded(jdoodleOutput.Output, taskDetails.Output);

            if(isCompleted && taskToCompile.IsSumbit)
            {
                try
                {
                    await SaveCompletedTask(taskToCompile, taskDetails, jdoodleOutput);
                }
                catch (Exception ex)
                {
                    return BadRequest("Can't save completed task.");
                }
            }

            var compiliedTask = new CompiledTask()
            {
                IsCompleted = isCompleted,
                StatusCode = jdoodleOutput.StatusCode,
                Output = jdoodleOutput.Output,
                Memory = jdoodleOutput.Memory,
                CpuTime = jdoodleOutput.CpuTime
            };

            return compiliedTask;
        }

        private async Task SaveCompletedTask(TaskToCompile taskToCompile, TaskDetails taskDetails, JdoodleOutput jdoodleOutput)
        {
            CompletedTaskItem completedTaskItem = new CompletedTaskItem()
            {
                UserId = taskToCompile.UserId,
                TaskItemId = taskDetails.TaskItemId,
                Script = taskToCompile.Script,
                Language = taskToCompile.Language,
                Memory = jdoodleOutput.Memory,
                CpuTime = jdoodleOutput.CpuTime
            };

           await _completedTasksRepository.Add(completedTaskItem);
        }
    }
}

