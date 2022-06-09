using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;

namespace ProgrammingCompetitionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskDetailsController : ControllerBase
    {
        private readonly ITaskDetailsRepository _repository;

        public TaskDetailsController(ITaskDetailsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TaskDetails/5
        [HttpGet("{taskItemId}")]
        public async Task<ActionResult<IEnumerable<TaskDetails>>> GetTaskDetails(Guid taskItemId)
        {
            var taskDetails = await _repository.GetByTaskItemId(taskItemId);
            return Ok(taskDetails);
        }


        // POST: api/TaskDetails
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<ActionResult<TaskDetails>> PostTaskDetails(TaskDetailsNew taskDetailsNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            TaskDetails taskDetails = new TaskDetails()
            {
                TaskItemId = taskDetailsNew.TaskItemId,
                MainScript = taskDetailsNew.MainScript,
                UserScript = taskDetailsNew.UserScript,
                Input = taskDetailsNew.Input,
                Output = taskDetailsNew.Output,
                Language = taskDetailsNew.Language,
            };

            await _repository.Add(taskDetails);

            return Ok(taskDetails);
        }

        // DELETE: api/TaskDetails/5
        [HttpDelete("{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTaskDetails(Guid id)
        {

            var taskDetails = await _repository.Remove(id);

            if (taskDetails == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
