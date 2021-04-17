using System;
using System.Linq;
using TaskManager.Entity;
using TaskManager.Factory;

namespace TaskManager.UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hi, this is task manager!");

            //Task manager size will be set in build time. 
            //Console.WriteLine("Task manager size : ");
            //var limitStr = Console.ReadLine();
            //int.TryParse(limitStr, out int limit);

            Console.WriteLine("Please select Customer Type: D for Default/ F for Fifo / P for Priority ");
            var customerType = Console.ReadLine();

            var taskManager = TaskManagerFactory.CreateTaskManager(customerType);


            PrintMenu();
            var command = Console.ReadLine();

            while (command != "E" && command != "e")
            {
                if (command == "A" || command == "a")
                {
                    NewProcess(taskManager);
                }
                else if (command == "L" || command == "l")
                {
                    ListProcesses(taskManager);
                }
                else if (command == "K" || command == "k")
                {
                    KillProcess(taskManager);
                }
                else
                {
                    Console.WriteLine("Wrong command.");
                }

                PrintMenu();
                command = Console.ReadLine();
            }

            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }

        private static void KillProcess(ITaskManager taskManager)
        {
            var processList = taskManager.List(process => process.CreateDate).ToList();
            if (!processList.Any())
            {
                Console.WriteLine("There are no running processes.");
            }
            else
            {
                processList.ForEach(k => Console.WriteLine($"Press {k.PID} to delete the process."));
                var idToDelete = Console.ReadLine();
                if (int.TryParse(idToDelete, out var pid))
                {
                    var killed = taskManager.Kill(pid);
                    Console.WriteLine(killed ? " Process killed" : " Process can't be killed");
                }
                else
                {
                    Console.WriteLine("Process not found.");
                }
            }
        }

        private static void ListProcesses(ITaskManager taskManager)
        {
            var processList = taskManager.List(process => process.CreateDate);
            processList.ToList().ForEach(k => Console.WriteLine(k.Print()));
        }

        private static void NewProcess(ITaskManager taskManager)
        {
            Console.WriteLine("Please select priority : 1 for Low / 2 for Medium / 3 for High");
            var priorityStr = Console.ReadLine();
            
            if (int.TryParse(priorityStr, out int priorityId) && Enum.IsDefined(typeof(Priority), priorityId))
            {
                var newProcess = taskManager.Add(new ProcessDto((Priority)priorityId));
                Console.WriteLine((newProcess != null)
                    ? $"New process added. {newProcess.Print()}"
                    : "Task manager is full.Process can't be added.");
            }
            else
            {
                Console.WriteLine("Invalid value for priority.");
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("Press A to add / L to List / K to Kill / E to Exit");
        }
    }
}