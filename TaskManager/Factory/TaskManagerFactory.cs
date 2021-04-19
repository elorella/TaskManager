namespace TaskManager.Factory
{
    public class TaskManagerFactory
    {
        public const int TaskManagerSize = 5;

        public static ITaskManager CreateTaskManager(string customer = default, int taskManagerSize = TaskManagerSize)
        {
            if (taskManagerSize < 1)
                taskManagerSize = TaskManagerSize;

            ITaskManager taskManager;
            switch (customer)
            {
                case "F":
                {
                    taskManager = new FiFoTaskManager(taskManagerSize);
                    break;
                }
                case "P":
                {
                    taskManager = new PriorityTaskManager(taskManagerSize);
                    break;
                }
                default:
                {
                    taskManager = new TaskManager(taskManagerSize);
                    break;
                }
            }

            return taskManager;
        }
    }
}