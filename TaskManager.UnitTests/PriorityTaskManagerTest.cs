using System.Linq;
using TaskManager.Entity;
using TaskManager.Factory;
using Xunit;

namespace TaskManager.UnitTests
{
    public class PriorityTaskManagerTest
    {
        private const string CustomerName = "P";

        [Fact]
        public void PriorityQueueWithLowPriority()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(CustomerName, 2);
            var p1 = new ProcessDto(Priority.Low);
            taskManager.Add(p1);
            taskManager.Add(new ProcessDto(Priority.Low));

            var isAdded = taskManager.Add(new ProcessDto(Priority.Medium));
            Assert.NotNull(isAdded);

            var processList = taskManager.List();
            Assert.Null(processList.FirstOrDefault(m => m.PID == p1.PID));
        }

        [Fact]
        public void PriorityQueueWithHighPriority()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(CustomerName, 2);
            var pLowOld = new ProcessDto(Priority.Low);
            var pHigh = new ProcessDto(Priority.High);
            var pMedium = new ProcessDto(Priority.Medium);
            var pLowNew = new ProcessDto(Priority.Low);

            taskManager.Add(pLowOld);
            taskManager.Add(pHigh);
            taskManager.Add(pLowNew);

            var isAdded = taskManager.Add(pMedium);
            Assert.NotNull(isAdded);

            var processList = taskManager.List();
            Assert.Null(processList.FirstOrDefault(m => m.PID == pLowOld.PID));
        }
    }
}