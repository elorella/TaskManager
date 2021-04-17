using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Entity;
using TaskManager.Factory;
using Xunit;

namespace TaskManager.UnitTests
{
    public class TaskManagerTests
    {
        [Fact(DisplayName = "List function should order by process createDate")]
        public void TaskListShouldBeOrderedByCreationDate()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager();
            var p1 = taskManager.Add(new ProcessDto(Priority.Low));

            var p2 = taskManager.Add(new ProcessDto(Priority.High));

            var p3 = taskManager.Add(new ProcessDto(Priority.Medium));

            var items = taskManager.List().ToList();
            Assert.Equal(items[0].PID, p1.PID);
            Assert.Equal(items[1].PID, p2.PID);
            Assert.Equal(items[2].PID, p3.PID);
        }

        [Fact(DisplayName = "List function should order by process Priority")]
        public void TaskListShouldBeOrderedByPriority()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager();
            var pLow = taskManager.Add(new ProcessDto(Priority.Low));

            var pHigh = taskManager.Add(new ProcessDto(Priority.High));

            var pMedium = taskManager.Add(new ProcessDto(Priority.Medium));

            var items = taskManager.List(m=>m.Priority).ToList();
            Assert.Equal(items[0].PID, pLow.PID);
            Assert.Equal(items[1].PID, pMedium.PID);
            Assert.Equal(items[2].PID, pHigh.PID);
        }

        [Fact(DisplayName = "List function should order by process PID")]
        public void TaskListShouldBeOrderedByPid()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager();
            
            var p1 = taskManager.Add(new ProcessDto(Priority.Low));

            var p2 = taskManager.Add(new ProcessDto(Priority.High));

            var p3 = taskManager.Add(new ProcessDto(Priority.Medium));

            var p4 = taskManager.Add(new ProcessDto(Priority.Medium));

            var items = taskManager.List(m => m.PID).ToList();
            Assert.Equal(items[0].PID, p1.PID);
            Assert.Equal(items[1].PID, p2.PID);
            Assert.Equal(items[2].PID, p3.PID);
            Assert.Equal(items[3].PID, p4.PID);

        }


        [Fact(DisplayName = "Default task manager should not allow overflow.")]
        public void TaskManagerOverFlow()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(taskManagerSize: 2);
            taskManager.Add(new ProcessDto(Priority.Low));
            taskManager.Add(new ProcessDto(Priority.Low));
            var isAdded = taskManager.Add(new ProcessDto(Priority.Low));
            Assert.Null(isAdded);

            var itemCount = taskManager.List().ToList().Count;
            Assert.True(itemCount == 2);
        }

        [Fact(DisplayName = "Killed process should be removed from task manager.")]
        private void KillShouldRemoveTheProcessFromTaskManager()
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(taskManagerSize: 2);
            var process = taskManager.Add(new ProcessDto(Priority.Low));

            var killed = taskManager.Kill(process.PID); 
            Assert.True(killed);

            var processlist = taskManager.List();
            Assert.Null(processlist.FirstOrDefault(m=>m.PID == process.PID));
        }

        [InlineData(Priority.High)]
        [InlineData(Priority.Medium)]
        [InlineData(Priority.Low)]
        [Theory(DisplayName = "Killed process with priority should remove all task with certain priority.")]
        private void KillByPriorityShouldRemoveTheProcessFromTaskManager(Priority priority)
        {
            var taskManager = TaskManagerFactory.CreateTaskManager(taskManagerSize: 6);

            taskManager.Add(new ProcessDto(Priority.Low));
            taskManager.Add(new ProcessDto(Priority.Low));
            taskManager.Add(new ProcessDto(Priority.Medium));
            taskManager.Add(new ProcessDto(Priority.Medium));
            taskManager.Add(new ProcessDto(Priority.High));
            taskManager.Add(new ProcessDto(Priority.High));

            var killed = taskManager.KillByPriority(priority);
            Assert.True(killed);

            var processlist = taskManager.List();
            Assert.Null(processlist.FirstOrDefault(m => m.Priority == priority));
        }
    }
}