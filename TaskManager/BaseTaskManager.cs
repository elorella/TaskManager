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
        //protected readonly Queue<Process> ProcessList

        protected BaseTaskManager(int count)
        {
            ProcessList = new List<Process>(count);
        }

        public ProcessDto Add(ProcessDto processDto)
        {
            var process = new Process(processDto.Priority, _newTaskId);

            if (ProcessList.Count == ProcessList.Capacity)
                if (!CanBeAdded(process))
                    return null;

            ProcessList.Add(process);
            _newTaskId++;

            return MapToDto(process);
        }

        /// <summary>
        ///     List of running process ordered by Creation Date
        /// </summary>
        public IEnumerable<ProcessDto> List()
        {
            return
                ProcessList.Select(MapToDto);
        }

        public IEnumerable<ProcessDto> List<TKey>(Func<ProcessDto, TKey> keySelector)
        {
            var ordered = List().OrderBy(keySelector);
            return ordered;
        }

        public bool Kill(int processId)
        {
            var processToKill = GetById(processId);
            if (processToKill == null)
                return false;

            var killed = processToKill.Kill();
            return killed && ProcessList.Remove(processToKill);
        }

        public bool KillByPriority(Priority priority)
        {
            var priorityList = GetByPriority(priority).ToList();
            priorityList.ForEach(m => Kill(m.PID));
            return true;
        }

        public bool KillAll()
        {
            ProcessList.ForEach(m => Kill(m.PID));
            return true;
        }

        public abstract bool CanBeAdded(Process process);

        private Process GetById(int pid) // REPO 
        {
            return ProcessList.FirstOrDefault(m => m.PID == pid);
        }

        private IEnumerable<Process> GetByPriority(Priority priority) // REPO 
        {
            return ProcessList.Where(m => m.Priority == priority);
        }

        private ProcessDto MapToDto(Process process) // Mapper 
        {
            return new ProcessDto(process.PID, process.Priority, process.CreateDate);
        }

    }
}