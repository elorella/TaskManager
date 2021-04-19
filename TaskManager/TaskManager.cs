using TaskManager.Entity;

namespace TaskManager
{
    public class TaskManager : BaseTaskManager
    {
        internal TaskManager(int count) : base(count)
        {
        }

        public override bool TryAddOverflow(Process process)
        {
            return false;
        }
    }
}