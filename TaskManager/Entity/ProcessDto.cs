using System;

namespace TaskManager.Entity
{
    public class ProcessDto
    {
        public ProcessDto(Priority priority)
        {
            Priority = priority;
        }

        internal ProcessDto(int pId, Priority priority, DateTime createDate)
        {
            Priority = priority;
            CreateDate = createDate;
            PID = pId;
        }

        public int PID { get; }
        public Priority Priority { get; }
        public DateTime CreateDate { get; set; }
        public override string ToString()
        {
            return $"PID:{PID} Priority:{Priority} Create Date:{CreateDate}";
        }
    }
}