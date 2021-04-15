using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Business.Model
{
    public class TaskManager : ITaskManager
    {
        private readonly List<Process> _processList= null;

        public TaskManager(int count )
        {
            _processList = new List<Process>(count);
        }

       public bool Add(Process process)
        {
            // default behaviour 
            if (_processList.Count < _processList.Capacity)
            {
                _processList.Add(process);
                return true;
            }

            return false;
        }

        public IList<Process> List()
        {
            return _processList;
        }

        public bool Kill(Process process)
        {
           return _processList.Remove(process);
        }
    }
}
