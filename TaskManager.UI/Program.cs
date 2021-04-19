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
            Console.WriteLine("Hi, this is the task manager!");
            
            try
            {
                Console.WriteLine("Please select Customer Type: F for Fifo / P for Priority / D for Default");
                var customerType = Console.ReadLine();
                var taskManager = TaskManagerFactory.CreateTaskManager(customerType);


                PrintMenu();
                var command = Console.ReadLine()?.ToUpper();

                while (command != "E")
                {
                    switch (command)
                    {
                        case "A":
                            NewProcess(taskManager);
                            break;
                        case "L":
                            ListProcesses(taskManager);
                            break;
                        case "K":
                            KillProcess(taskManager);
                            break;
                        default:
                            Console.WriteLine("Wrong command.");
                            break;
                    }

                    PrintMenu();
                    command = Console.ReadLine();
                }

                Console.WriteLine("Bye!");
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            processList.ToList().ForEach(k => Console.WriteLine(k.ToString()));
        }

        private static void NewProcess(ITaskManager taskManager)
        {
            Console.WriteLine("Please select priority : 1 for Low / 2 for Medium / 3 for High");
            var priorityStr = Console.ReadLine();

            if (ValidateEnumInput(priorityStr))
            {
                var priorityId = int.Parse(priorityStr);
                var newProcess = taskManager.Add(new ProcessDto((Priority) priorityId));
                var outputMessage = newProcess != null
                    ? $"New process added. {newProcess.ToString()}"
                    : "Task manager is full.Process can't be added.";
                Console.WriteLine(outputMessage);
            }
            else
            {
                Console.WriteLine("Invalid value for priority.");
            }
        }

        private static bool ValidateEnumInput(string priorityStr)
        {
            if (string.IsNullOrWhiteSpace(priorityStr))
                return false;

            return int.TryParse(priorityStr, out var priorityId) && Enum.IsDefined(typeof(Priority), priorityId);
        }

        private static void PrintMenu()
        {
            Console.WriteLine("Press A to add / L to List / K to Kill / E to Exit");
        }
    }
}