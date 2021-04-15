using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Business.Model
{
    public class FiFoTaskManager : ITaskManager
    {
        private readonly List<Process> _processList= null;

        public FiFoTaskManager(int count )
        {
            _processList = new List<Process>(count);
        }

       public bool Add(Process process)
        {
            // killing and removing from the TM list
            if (_processList.Count == _processList.Capacity)
            {
                // Kill the oldest 
                var oldestProcess = _processList.OrderBy(m => m.CreateDate).First();
                Kill(oldestProcess);
            }
            _processList.Add(process);
            return true;
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
