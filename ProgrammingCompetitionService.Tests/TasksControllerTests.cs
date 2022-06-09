using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ProgrammingCompetitionService.Controllers;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;


namespace ProgrammingCompetitionService.Tests
{
    public class TasksControllerTests
    {
        private readonly ITasksRepository _repository;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
             _repository = A.Fake<ITasksRepository>();
             _controller = new TasksController(_repository);
        }

        [Fact]
        public async Task GetTasks_ReturnFiveTasks()
        {
            //Arrange
            int count = 5;
            var fakeTasks = A.CollectionOfDummy<TaskItem>(count).AsEnumerable();
            A.CallTo(() => _repository.GetAll()).Returns(Task.FromResult(fakeTasks));

            //Act
            var actionResult = await _controller.GetTasks();

            //Assert
            var result = actionResult.Result as OkObjectResult;
            var returnTasks = result.Value as IEnumerable<TaskItem>;

            Assert.Equal(count, returnTasks.Count());
        }

        [Fact]
        public async Task PostTaskItem_InputAndOutputTask_IsEqual()
        {
            //Arrange
            TaskItemNew taskItemNew = new TaskItemNew()
            {
                Name = "test"
            };
            TaskItem taskItem = new TaskItem()
            {
                Name = taskItemNew.Name,
                Description = taskItemNew.Description
            };
            A.CallTo(() => _repository.Add(taskItem)).Returns(Task.FromResult(taskItem));
            //Act
            var actionResult = await _controller.PostTaskItem(taskItemNew);
            //Assert
            var result = actionResult.Result as OkObjectResult;
            var returnTask = result.Value as TaskItem;
            Assert.Equal(taskItemNew.Name, returnTask.Name);
        }

        [Fact]
        public async Task DeleteTaskItem_Return_NoContent()
        {
            //Arrange
            TaskItem taskItem = new TaskItem()
            {
                TaskItemId = new Guid()
            };
            A.CallTo(() => _repository.Add(taskItem)).Returns(Task.FromResult(taskItem));
            //Action
            var actionResult = await _controller.DeleteTaskItem(taskItem.TaskItemId);
            //Assert
            Assert.IsType<NoContentResult>(actionResult);        
        }
    }
}