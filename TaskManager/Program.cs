using System;
using System.Linq;
using TaskManager.Business;
using TaskManager.Business.Model;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi, this is task manager!");
            Console.WriteLine("Max limit : ");
            var limitstr = Console.ReadLine();
            var limit = Convert.ToInt32(limitstr);

            Console.WriteLine("Customer Type: D default/ F Fifo / P Priority ");
            var customerType = Console.ReadLine();

            ITaskManager taskManager = null;
            switch (customerType)
            {
                case "F":
                {
                    taskManager = new Business.Model.FiFoTaskManager(limit);
                    break;
                }
                case "P":
                {
                    taskManager = new Business.Model.PriorityTaskManager(limit);
                    break;
                }
                default:
                {
                    taskManager = new Business.Model.TaskManager(limit);
                    break;
                }
            }


            Console.WriteLine("Press A to add / L to List / E to Exit");
            var command = Console.ReadLine();
            
            while (command != "E")
            {
                if (command == "A")
                {
                    var newProcess = new Process() {PID = Guid.NewGuid(), Priority = Priority.Low};
                    var isAdded = taskManager.Add(newProcess);
                    Console.WriteLine(isAdded ? $"New Task added with PID  {newProcess.PID} - {newProcess.Priority} - Creation:{newProcess.CreateDate}" : $"Task can't be added.");
                }
                else if (command == "L")
                {
                    var processList = taskManager.List();
                    int i = 1;
                    processList.ToList().ForEach(k => Console.WriteLine($"{i++}-{k.PID}"));
                }
                else if (command == "K")
                {
                    var processList = taskManager.List();
                    int i = 1;
                    processList.ToList().ForEach(k => Console.WriteLine($"{i++}-{k.PID}"));
                }
                else
                {
                    Console.WriteLine("Wrong command.");
                }

                Console.WriteLine("Press A to add/ L To List/ E to Exit");
                command = Console.ReadLine();
            }

            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }
}
