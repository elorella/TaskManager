using System;
using TaskManager.Entity;
using TaskManager.Factory;
using Xunit;

namespace TaskManager.UnitTests
{
    public class TaskManagerFactoryProviderTest
    {
        [Theory(DisplayName = "Factory should create a task manager according to customer")]
        [InlineData("D", typeof(TaskManager))]
        [InlineData("F", typeof(FiFoTaskManager))]
        [InlineData("P", typeof(PriorityTaskManager))]
        [InlineData("Undefined", typeof(TaskManager))]
        [InlineData(null, typeof(TaskManager))]
        public void FactoryShouldCreateTaskManagersBasedOnCustomer(string customer, Type typeOfTaskManager)
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(customer);
            Assert.Equal(typeOfTaskManager, taskManager.GetType());
        }

        [Theory(DisplayName = "Factory should create the task manager with the limit defined")]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(100)]
        public void FactoryShouldCreateTaskManagersWithTheGivenSize(int size)
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(taskManagerSize: size);
            for (var i = 0; i < size; i++) {taskManager.Add(new ProcessDto(Priority.Low));}
            var isAdded = taskManager.Add(new ProcessDto(Priority.Low));
            Assert.Null(isAdded);
        }

        [Fact(DisplayName = "Factory, should create a Task manager with default size.")]
        public void FactoryShouldCreateTaskManagersWithDefaultSize()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager();

            for (var i = 0; i < TaskManagerFactory.TaskManagerSize; i++)
                taskManager.Add(new ProcessDto(Priority.Low));

            var isAdded = taskManager.Add(new ProcessDto(Priority.Low));
            Assert.Null(isAdded);
        }
    }
}