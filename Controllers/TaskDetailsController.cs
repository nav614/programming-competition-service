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
    public class TaskDetailsController : ControllerBase
    {
        private readonly PCContext _context;

        public TaskDetailsController(PCContext context)
        {
            _context = context;
        }

        // GET: api/TaskDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TaskDetails>>> GetTaskDetails(Guid id)
        {
            if (_context.TaskDetails == null)
            {
                return NotFound();
            }
            return await _context.TaskDetails.Where(x => x.TaskItemId == id).ToListAsync();
        }


        // POST: api/TaskDetails
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<ActionResult<TaskDetails>> PostTaskDetails(TaskDetailsNew taskDetailsNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_context.TaskDetails == null)
            {
                return Problem("askDetails is null.");
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

            _context.TaskDetails.Add(taskDetails);
            await _context.SaveChangesAsync();

            return Ok(taskDetails);
        }

        // DELETE: api/TaskDetails/5
        [HttpDelete("{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTaskDetails(Guid id)
        {
            if (_context.TaskDetails == null)
            {
                return NotFound();
            }
            var taskDetails = await _context.TaskDetails.FindAsync(id);
            if (taskDetails == null)
            {
                return NotFound();
            }

            _context.TaskDetails.Remove(taskDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
