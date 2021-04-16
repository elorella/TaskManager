﻿using System.Collections.Generic;
using TaskManager.Business.Model;

namespace TaskManager.Business
{
    public abstract class BaseTaskManager : ITaskManager
    {
        protected readonly List<Process> ProcessList = null;

        protected BaseTaskManager(int count)
        {
            ProcessList = new List<Process>(count);
        }

        public bool Add(Process process)
        {
            // default behaviour 
            if (ProcessList.Count == ProcessList.Capacity)
                if (!CanAddBeAdded(process))
                    return false;

            ProcessList.Add(process);
            return true;
        }

        public IList<Process> List()
        {
            return ProcessList;
        }

        public bool Kill(Process process)
        {
            var killed = process.Kill();
            return killed && ProcessList.Remove(process);
        }

        public virtual bool CanAddBeAdded(Process process)
        {
            return false;
        }
    }
}