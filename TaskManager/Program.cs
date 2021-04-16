using System;
using System.Linq;
using TaskManager.Business;
using TaskManager.Business.Model;

namespace TaskManager
{
    internal class Program
    {
        private static void Main(string[] args)
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
                    taskManager = new FiFoTaskManager(limit);
                    break;
                }
                case "P":
                {
                    taskManager = new PriorityTaskManager(limit);
                    break;
                }
                default:
                {
                    taskManager = new Business.TaskManager(limit);
                    break;
                }
            }


            Console.WriteLine("Press A to add / L to List/ / K to Kill / E to Exit");
            var command = Console.ReadLine();

            while (command != "E")
            {
                if (command == "A")
                {
                    var newProcess = new Process(Guid.NewGuid(), Priority.Low);


                    var isAdded = taskManager.Add(newProcess);
                    Console.WriteLine(isAdded
                        ? $"New Task added with PID  {newProcess.PID} - {newProcess.Priority} - Creation:{newProcess.CreateDate}"
                        : $"Task can't be added.");
                }
                else if (command == "L")
                {
                    var processList = taskManager.List();
                    var i = 1;
                    processList.ToList().ForEach(k => Console.WriteLine($"{i++}-{k.PID}"));
                }
                else if (command == "K")
                {
                    var processList = taskManager.List().ToList();
                    var i = 1;
                    processList.ForEach(k => Console.WriteLine($"Press {i++} to delete {k.PID}"));

                    var indexToDelete = Console.ReadLine();
                    if (int.TryParse(indexToDelete, out int index))
                    {
                        bool killed = taskManager.Kill(processList[index-1]);
                    }
                    else
                    {
                        Console.WriteLine("Process not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong command.");
                }

                Console.WriteLine("Press A to add / L to List/ / K to Kill / E to Exit");
                command = Console.ReadLine();
            }

            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }
}