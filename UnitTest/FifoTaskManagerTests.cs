using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Business.Model;

namespace UnitTest
{
    [TestClass]
    public class FifoTaskManagerTests
    {
        [TestMethod("3. task can be added to FIFO task manager.")]
        public void FifoTaskManagerOverFlow()
        {
            var taskManager = new TaskManager.Business.FiFoTaskManager(2);
            taskManager.Add(new Process(Guid.NewGuid(), Priority.Low));
            taskManager.Add(new Process(Guid.NewGuid(), Priority.Low));

            var isAdded = taskManager.Add(new Process(Guid.NewGuid(), Priority.Low));
            Assert.IsTrue(isAdded);

            var itemCount = taskManager.List().ToList().Count;
            Assert.AreEqual(2, itemCount);
        }

        [TestMethod("When there is an overflow oldest process will be killed.")]
        public void FifoTaskManagerOverFlowOldestWillBeKilled()
        {
            var taskManager = new TaskManager.Business.FiFoTaskManager(2);
            var p1 = new Process(Guid.NewGuid(), Priority.Low);
            var p2 = new Process(Guid.NewGuid(), Priority.Low);
            var p3 = new Process(Guid.NewGuid(), Priority.Low);
            taskManager.Add(p1);
            taskManager.Add(p2);
            taskManager.Add(p3);

            var processList = taskManager.List();
            Assert.IsNull(processList.FirstOrDefault(m => m.PID == p1.PID));

            var p4 = new Process(Guid.NewGuid(), Priority.Low);
            taskManager.Add(p4);
            processList = taskManager.List();
            Assert.IsNull(processList.FirstOrDefault(m => m.PID == p2.PID));
        }
    }
}