using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Business.Model
{
    public class PriorityTaskManager : ITaskManager
    {
        private readonly List<Process> _processList= null;

        public PriorityTaskManager(int count )
        {
            _processList = new List<Process>(count);
        }

        public bool Add(Process process)
        {
            //if the new process passed in the add() call has a higher priority compared to any of the
            //existing one, we remove the lowest priority that is the oldest, otherwise we skip it
            if (_processList.Count == _processList.Capacity)
            {
                var lowerPriority = _processList.OrderBy(m => m.CreateDate)
                    .FirstOrDefault(m =>  m.Priority <  process.Priority);
                if (lowerPriority != null)
                {
                    Kill(lowerPriority);
                }
                else
                {
                    return false;
                }
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
            process.Kill();
           return _processList.Remove(process);
        }
    }
}
