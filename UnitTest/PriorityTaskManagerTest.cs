using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Business.Model;

namespace UnitTest
{
    [TestClass]
    public class PriorityTaskManagerTest
    {
        [TestMethod("When overflow, medium priority process can be added to a low priority queue")]
        public void PriorityQueueWithLowPriority()
        {
            var taskManager = new TaskManager.Business.PriorityTaskManager(2);
            var p1 = new Process(Guid.NewGuid(), Priority.Low);
            taskManager.Add(p1);
            taskManager.Add(new Process(Guid.NewGuid(), Priority.Low));

            var isAdded = taskManager.CanAddBeAdded(new Process(Guid.NewGuid(), Priority.Medium));
            Assert.IsTrue(isAdded);

            var processList = taskManager.List();
            Assert.IsNull(processList.FirstOrDefault(m => m.PID == p1.PID));
        }

        [TestMethod("When overflow, medium priority process can be if there is a low priority process running.")]
        public void PriorityQueueWithHighPriority()
        {
            var taskManager = new TaskManager.Business.PriorityTaskManager(2);
            var pLowOld = new Process(Guid.NewGuid(), Priority.Low);
            var pHigh = new Process(Guid.NewGuid(), Priority.High);
            var pMedium = new Process(Guid.NewGuid(), Priority.Medium);
            var pLowNew = new Process(Guid.NewGuid(), Priority.Low);

            taskManager.Add(pLowOld);
            taskManager.Add(pHigh);
            taskManager.Add(pLowNew);

            var isAdded = taskManager.Add(pMedium);
            Assert.IsTrue(isAdded);

            var processList = taskManager.List();
            Assert.IsNull(processList.FirstOrDefault(m => m.PID == pLowOld.PID));
        }
    }
}