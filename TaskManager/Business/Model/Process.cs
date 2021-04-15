using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Business.Model
{
    public class Process
    {
        public Guid PID { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreateDate => DateTime.Now;

        public bool Kill()
        {
            Console.WriteLine($"I'm dead! {PID} - {Priority} - Creation:{CreateDate}");
            return false;
        }
    }

    public enum Priority
    {
       Low = 1,
       Medium =3,
       High = 3
    }
}
