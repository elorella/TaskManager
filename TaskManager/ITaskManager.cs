using System;
using System.Collections.Generic;
using TaskManager.Entity;

namespace TaskManager
{
    public interface ITaskManager
    {
        public ProcessDto Add(ProcessDto process);
        IEnumerable<ProcessDto> List();
        public IEnumerable<ProcessDto> List<TKey>(Func<ProcessDto, TKey> keySelector);
        public bool Kill(int processId);
        public bool KillByPriority(Priority priority);
        public bool KillAll();
    }
}