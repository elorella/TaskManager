using System.Linq;
using TaskManager.Entity;

namespace TaskManager
{
    public class FiFoTaskManager : BaseTaskManager
    {
        internal FiFoTaskManager(int count) : base(count)
        {
        }

        public override bool TryAddOverflow(Process process)
        {
            var oldestProcess = ProcessList.OrderBy(m => m.CreateDate).First();
            Kill(oldestProcess.PID);
            return true;
        }
    }
}