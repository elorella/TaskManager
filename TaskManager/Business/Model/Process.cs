using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Business.Model
{
    public class Process
    {
        public Guid PID { get; }
        public Priority Priority { get; }
        public DateTime CreateDate => DateTime.Now;

        public Process(Guid pid, Priority priority)
        {
            PID = pid;
            Priority = priority;
        }

        internal bool Kill()
        {
            Console.WriteLine($"I'm dead! {PID} - {Priority} - Creation:{CreateDate}");
            return true;
        }
    }
}