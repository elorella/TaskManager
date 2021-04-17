using TaskManager.Entity;

namespace TaskManager.UI
{
    public static class Extensions
    {
        public static string Print(this ProcessDto process)
        {
            return $"PID:{process.PID} Priority:{process.Priority} Create Date:{process.CreateDate}";
        }
    }
}