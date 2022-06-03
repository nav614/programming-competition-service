using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly PCContext _context;

        public TasksController(PCContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            return await _context.Tasks.ToListAsync();
        }


        // POST: api/Tasks
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItemNew taskItemNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_context.Tasks == null)
            {
                return Problem("Tasks is null.");
            }

            TaskItem taskItem = new TaskItem() { Name = taskItemNew.Name, Description = taskItemNew.Description };

            _context.Tasks.Add(taskItem);
            await _context.SaveChangesAsync();

            return Ok(taskItem);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
