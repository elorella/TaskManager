using System;

namespace TaskManager.Entity
{
    public class Process
    {
        internal Process(Priority priority, int pid)
        {
            Priority = priority;
            CreateDate = DateTime.Now;
            PID = pid; 
        }

        public int PID { get; }
        public Priority Priority { get; }
        public DateTime CreateDate { get; }

        internal bool Kill()
        {
            Console.WriteLine($"I'm dead! {PID} - {Priority} - Creation:{CreateDate}");
            return true;
        }
    }
}