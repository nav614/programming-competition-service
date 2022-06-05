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
    public class CompletedTasksController : ControllerBase
    {
        private readonly PCContext _context;

        public CompletedTasksController(PCContext context)
        {
            _context = context;
        }

        // GET: api/CompletedTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedTaskItem>>> GetCompletedTasks()
        {
            if (_context.CompletedTasks == null)
            {
                return NotFound();
            }
            return await _context.CompletedTasks.ToListAsync();
        }

        // GET: api/topthree
        [HttpGet("topthree")]
        public async Task<ActionResult<IEnumerable<UserResults>>> GetTopThree()
        {
            if (_context.CompletedTasks == null)
            {
                return NotFound();
            }

            var query = (from completedTasks in _context.CompletedTasks.ToList()
                         group completedTasks by completedTasks.UserId into ctg
                         join users in _context.Users.ToList() on ctg.FirstOrDefault().UserId equals users.UserId
                         orderby ctg.Count() descending
                         select new UserResults
                         {
                             Username = users.UserName,
                             Score = ctg.Count(),
                             CompletedTasks = string.Join(", ", (from c in ctg
                                                                join t in _context.Tasks.ToList() on c.TaskItemId equals t.TaskItemId
                                                                select t.Name).ToArray())

                         }).Take(3);

            return query.ToList();
        }

    }
}
