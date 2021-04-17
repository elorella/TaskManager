using TaskManager.Entity;

namespace TaskManager
{
    public class TaskManager : BaseTaskManager
    {
        internal TaskManager(int count) : base(count)
        {
        }

        public override bool CanBeAdded(Process process)
        {
            return false;
        }
    }
}