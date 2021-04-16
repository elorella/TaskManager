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

            var itemCount = taskManager.List().ToList().Count;
            Assert.AreEqual(itemCount, 2);
        }

        [TestMethod("Task list has to be ordered by creation date.")]
        public void TaskListShouldBeOrderedByCreationDate()
        {
            var taskManager = new global::TaskManager.Business.TaskManager(2);
            var p1 = new Process(Guid.NewGuid(), Priority.Low);
            taskManager.Add(p1);

            var p2 = new Process(Guid.NewGuid(), Priority.High);
            taskManager.Add(p2);

            var p3 = new Process(Guid.NewGuid(), Priority.Medium);
            taskManager.Add(p3);
           
            var items = taskManager.List().ToList();
            Assert.AreEqual(items[0].PID, p1.PID);
            Assert.AreEqual(items[1].PID, p2.PID);
            Assert.AreEqual(items[2].PID, p3.PID);
        }
    }
}