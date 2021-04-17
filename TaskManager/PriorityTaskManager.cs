using System.Linq;
using TaskManager.Entity;

namespace TaskManager
{
    public class PriorityTaskManager : BaseTaskManager
    {
        internal PriorityTaskManager(int count) : base(count)
        {
        }

        public override bool CanBeAdded(Process process)
        {
            //if the new process passed in the add() call has a higher priority compared to any of the
            //existing one, we remove the lowest priority that is the oldest, otherwise we skip it

            var lowerPriority = ProcessList.OrderBy(m => m.CreateDate)
                .FirstOrDefault(m => m.Priority < process.Priority);
            if (lowerPriority != null)
            {
                Kill(lowerPriority.PID);
                return true;
            }

            return false;
        }
    }
}