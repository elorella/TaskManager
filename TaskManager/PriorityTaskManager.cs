using System.Linq;
using TaskManager.Entity;

namespace TaskManager
{
    public class PriorityTaskManager : BaseTaskManager
    {
        internal PriorityTaskManager(int count) : base(count)
        {
        }

        public override bool TryAddOverflow(Process process)
        {
            var lowerPriority = ProcessList.OrderBy(m => m.CreateDate)
                .FirstOrDefault(m => m.Priority < process.Priority);
            if (lowerPriority == null)
            {
                return false;
            }

            Kill(lowerPriority.PID);
            return true;
        }
    }
}