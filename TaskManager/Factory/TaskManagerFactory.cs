namespace TaskManager.Factory
{
    public class TaskManagerFactory
    {
        public const int BuildTimeTaskManagerSize = 5;

        public static ITaskManager CreateTaskManager(string customer = default, int taskManagerSize = BuildTimeTaskManagerSize)
        {
            if (taskManagerSize < 0)
                taskManagerSize = BuildTimeTaskManagerSize;

            ITaskManager taskManager = null;
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