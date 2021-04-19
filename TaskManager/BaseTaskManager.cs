using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Entity;

namespace TaskManager
{
    public abstract class BaseTaskManager : ITaskManager
    {
        private static int _newTaskId = 1;
        protected readonly List<Process> ProcessList;

        protected BaseTaskManager(int count)
        {
            ProcessList = new List<Process>(count);
        }

        /// <summary>
        ///  Adds a new process to task-list
        /// </summary>
        public ProcessDto Add(ProcessDto processDto)
        {
            var process = new Process(processDto.Priority, _newTaskId);

            if (ProcessList.Count == ProcessList.Capacity)
                if (!TryAddOverflow(process))
                    return null;

            ProcessList.Add(process);
            _newTaskId++;

            return MapToDto(process);
        }

        /// <summary>
        ///     List of running process
        /// </summary>
        public IEnumerable<ProcessDto> List()
        {
            return
                ProcessList.Select(MapToDto);
        }

        /// <summary>
        ///     List of running process order by properties
        /// </summary>
        public IEnumerable<ProcessDto> List<TKey>(Func<ProcessDto, TKey> keySelector)
        {
            var ordered = List().OrderBy(keySelector);
            return ordered;
        }


        /// <summary>
        ///     Remove a task with the given Id from task manager
        /// </summary>
        public bool Kill(int processId)
        {
            var processToKill = GetById(processId);
            if (processToKill == null)
                return false;

            var killed = processToKill.Kill();
            return killed && ProcessList.Remove(processToKill);
        }

        /// <summary>
        ///     Remove task(s) with the given priority from task manager
        /// </summary>
        public bool KillByPriority(Priority priority)
        {
            var priorityList = GetByPriority(priority).ToList();
            priorityList.ForEach(m => Kill(m.PID));
            return true;
        }

        /// <summary>
        ///  Remove tasks from task manager. Failure cases are ignored. 
        /// </summary>
        public bool KillAll()
        {
            ProcessList.ToList().ForEach(m => Kill(m.PID));
            return true;
        }

        public abstract bool TryAddOverflow(Process process);

        // Can ben moved to a repository 
        private Process GetById(int pid) 
        {
            return ProcessList.FirstOrDefault(m => m.PID == pid);
        }

        // Can ben moved to a repository 
        private IEnumerable<Process> GetByPriority(Priority priority) 
        {
            return ProcessList.Where(m => m.Priority == priority);
        }

        // Can ben moved to a mapper 
        private ProcessDto MapToDto(Process process) // Mapper 
        {
            return new ProcessDto(process.PID, process.Priority, process.CreateDate);
        }
    }
}