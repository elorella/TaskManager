using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Business.Model;

namespace UnitTest
{
    [TestClass]
    public class TaskManagerTests
    {
        [TestMethod("3. task can't be added to task manager.")]
        public void TaskManagerOverFlow()
        {
            var taskManager = new global::TaskManager.Business.TaskManager(2);
            taskManager.Add(new Process(Guid.NewGuid(), Priority.Low));
            taskManager.Add(new Process(Guid.NewGuid(), Priority.Low));
            var isAdded = taskManager.CanAddBeAdded(new Process(Guid.NewGuid(), Priority.Low));
            Assert.IsFalse(isAdded);

            var itemCount = taskManager.List().Count;
            Assert.AreEqual(itemCount, 2);
        }
    }
}