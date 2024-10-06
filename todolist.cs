using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Task> todoList = new List<Task>();
    static string filePath = "todoList.txt";

    static void Main(string[] args)
    {
        LoadTasks();

        string command;

        do
        {
            Console.WriteLine("To-Do List Application");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Remove Task");
            Console.WriteLine("4. Mark Task as Completed");
            Console.WriteLine("5. Clear Completed Tasks");
            Console.WriteLine("6. Sort Tasks");
            Console.WriteLine("7. Save Tasks");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option (1-8): ");
            command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    ViewTasks();
                    break;

                case "3":
                    RemoveTask();
                    break;

                case "4":
                    MarkTaskAsCompleted();
                    break;

                case "5":
                    ClearCompletedTasks();
                    break;

                case "6":
                    SortTasks();
                    break;

                case "7":
                    SaveTasks();
                    break;

                case "8":
                    SaveTasks();
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (command != "8");
    }

    static void AddTask()
    {
        Console.Write("Enter task: ");
        string taskName = Console.ReadLine();
        todoList.Add(new Task(taskName));
        Console.WriteLine($"Task '{taskName}' added!");
    }

    static void ViewTasks()
    {
        Console.WriteLine("Your Tasks:");
        if (todoList.Count == 0)
        {
            Console.WriteLine("No tasks in your list.");
        }
        else
        {
            for (int i = 0; i < todoList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. [{(todoList[i].IsCompleted ? "X" : " ")}] {todoList[i].Name}");
            }
        }
    }

    static void RemoveTask()
    {
        Console.Write("Enter task number to remove: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= todoList.Count)
        {
            string removedTask = todoList[taskNumber - 1].Name;
            todoList.RemoveAt(taskNumber - 1);
            Console.WriteLine($"Task '{removedTask}' removed!");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void MarkTaskAsCompleted()
    {
        Console.Write("Enter task number to mark as completed: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= todoList.Count)
        {
            todoList[taskNumber - 1].IsCompleted = true;
            Console.WriteLine($"Task '{todoList[taskNumber - 1].Name}' marked as completed!");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void ClearCompletedTasks()
    {
        todoList.RemoveAll(t => t.IsCompleted);
        Console.WriteLine("All completed tasks have been cleared!");
    }

    static void SortTasks()
    {
        Console.WriteLine("Sort by:");
        Console.WriteLine("1. Task Name");
        Console.WriteLine("2. Completion Status");
        Console.Write("Choose an option (1-2): ");
        string sortOption = Console.ReadLine();

        switch (sortOption)
        {
            case "1":
                todoList.Sort((x, y) => string.Compare(x.Name, y.Name));
                Console.WriteLine("Tasks sorted by name.");
                break;

            case "2":
                todoList.Sort((x, y) => x.IsCompleted.CompareTo(y.IsCompleted));
                Console.WriteLine("Tasks sorted by completion status.");
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    static void SaveTasks()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in todoList)
            {
                writer.WriteLine($"{task.Name}|{task.IsCompleted}");
            }
        }
        Console.WriteLine("Tasks saved to file.");
    }

    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2 && bool.TryParse(parts[1], out bool isCompleted))
                    {
                        todoList.Add(new Task(parts[0], isCompleted));
                    }
                }
            }
            Console.WriteLine("Tasks loaded from file.");
        }
    }
}

class Task
{
    public string Name { get; set; }
    public bool IsCompleted { get; set; }

    public Task(string name, bool isCompleted = false)
    {
        Name = name;
        IsCompleted = isCompleted;
    }
}
