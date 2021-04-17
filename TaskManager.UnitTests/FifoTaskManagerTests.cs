using System.Linq;
using TaskManager.Entity;
using TaskManager.Factory;
using Xunit;

namespace TaskManager.UnitTests
{
    public class FifoTaskManagerTests
    {
        private const string CustomerName = "F";

        [Fact]
        public void FifoTaskManagerOverFlow()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(CustomerName, 2);
            taskManager.Add(new ProcessDto(Priority.Low));
            taskManager.Add(new ProcessDto(Priority.Low));

            var isAdded = taskManager.Add(new ProcessDto(Priority.Low));
            Assert.NotNull(isAdded);

            var itemCount = taskManager.List().ToList().Count;
            Assert.True(2 == itemCount);
        }

        [Fact]
        public void FifoTaskManagerOverFlowOldestWillBeKilled()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(CustomerName, 2);
            var p1 = new ProcessDto(Priority.Low);
            var p2 = new ProcessDto(Priority.Low);
            var p3 = new ProcessDto(Priority.Low);
            taskManager.Add(p1);
            taskManager.Add(p2);
            taskManager.Add(p3);

            var processList = taskManager.List(m => m.CreateDate);
            Assert.Null(processList.FirstOrDefault(m => m.PID == p1.PID));

            var p4 = new ProcessDto(Priority.Low);
            taskManager.Add(p4);
            processList = taskManager.List(m => m.CreateDate);
            Assert.Null(processList.FirstOrDefault(m => m.PID == p2.PID));
        }
    }
}