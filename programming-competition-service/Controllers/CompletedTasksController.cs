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
    public class CompletedTasksController : ControllerBase
    {
        private readonly ICompletedTasksRepository _completedTasksRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ITasksRepository _tasksRepository;


        public CompletedTasksController(ICompletedTasksRepository completedTasksRepository, IAuthRepository authRepository, ITasksRepository tasksRepository)
        {
            _completedTasksRepository = completedTasksRepository;
            _authRepository = authRepository;
            _tasksRepository = tasksRepository;
        }

        // GET: api/CompletedTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedTaskItem>>> GetCompletedTasks()
        {
            var result = await _completedTasksRepository.GetAll();

            return Ok(result);
        }

        // GET: api/topthree
        [HttpGet("topthree")]
        public async Task<ActionResult<IEnumerable<UserResults>>> GetTopThree()
        {
            var taskList = await _tasksRepository.GetAll();
            var completedTaskList = await _completedTasksRepository.GetAll();
            var userList = await _authRepository.GetAll();

            var query = (from completedTasks in completedTaskList
                         group completedTasks by completedTasks.UserId into ctg
                         join users in userList on ctg.FirstOrDefault().UserId equals users.UserId
                         orderby ctg.Count() descending
                         select new UserResults
                         {
                             Username = users.UserName,
                             Score = ctg.Count(),
                             CompletedTasks = string.Join(", ", (from c in ctg
                                                                 join t in taskList on c.TaskItemId equals t.TaskItemId
                                                                 select t.Name).ToArray())

                         }).Take(3);

            return query.ToList();
        }

    }
}
