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
            //Process will run its internal routine to be ready to be killed.
            //Entity shouldn't depend on Console but I'll add a row to notify the end-user. 
            Console.WriteLine($"I'm dead! {PID} - {Priority} - Creation:{CreateDate}");
            return true;
        }
    }
}