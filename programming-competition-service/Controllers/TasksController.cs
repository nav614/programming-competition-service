using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingCompetitionService.Intrerfaces;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITasksRepository _repository;

        public TasksController(ITasksRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _repository.GetAll();

            return Ok(tasks);
        }

        // POST: api/Tasks
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItemNew taskItemNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            TaskItem taskItem = new TaskItem()
            {
                Name = taskItemNew.Name,
                Description = taskItemNew.Description
            };

            await _repository.Add(taskItem);

            return Ok(taskItem);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            var taskItem = await _repository.Remove(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
