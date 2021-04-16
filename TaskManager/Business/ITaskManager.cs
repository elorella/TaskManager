using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Business.Model;

namespace TaskManager.Business
{
    public interface ITaskManager
    {
        public bool Add(Process process);
        public IList<Process> List();
        public bool Kill(Process process);
    }
}