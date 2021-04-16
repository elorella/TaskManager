using System.Linq;
using TaskManager.Business.Model;

namespace TaskManager.Business
{
    public class PriorityTaskManager : BaseTaskManager
    {
        public PriorityTaskManager(int count) : base(count)
        {
        }

        public override bool CanAddBeAdded(Process process)
        {
            //if the new process passed in the add() call has a higher priority compared to any of the
            //existing one, we remove the lowest priority that is the oldest, otherwise we skip it

            var lowerPriority = ProcessList.OrderBy(m => m.CreateDate)
                .FirstOrDefault(m => m.Priority < process.Priority);
            if (lowerPriority != null)
            {
                Kill(lowerPriority);
                return true;
            }

            return false;
        }
    }
}