using System.Collections.Generic;
using System.Linq;
using TaskManager.Business.Model;

namespace TaskManager.Business
{
    public class FiFoTaskManager : BaseTaskManager
    {
        public FiFoTaskManager(int count) : base(count)
        {
        }

        public override bool CanAddBeAdded(Process process)
        {
            var oldestProcess = ProcessList.OrderBy(m => m.CreateDate).First();
            Kill(oldestProcess);
            return true;
        }
    }
}