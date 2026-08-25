
public class Task
{
    public int Id;
     public string Description;
     public string Status;
}
class Program
{
    static List<Task> tasks = new List<Task>();
    static void AddTask()
    {
        Task task = new Task();
        Console.Write("Enter ID");
        task.Id = int.Parse(Console.ReadLine());
        Console.Write("Enter Description");
        task.Description = Console.ReadLine();
        task.Status = "Pending";
        tasks.Add(task);
        Console.WriteLine("Task added");
    }
    static void ListTasks()
    {
        foreach (Task task in tasks)
        {
            Console.WriteLine("ID: " + task.Id);
            Console.WriteLine("Description: " + task.Description);
            Console.WriteLine("Status: " + task.Status);
        }
    }
    static void CompleteTask()
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());
        foreach (Task task in tasks)
        {
            if (task.Id == id)
            {
                task.Status = "Completed";
                Console.WriteLine("Task is completed");
                return;
            }
        }
        Console.WriteLine("Task is not found");
    }
    static void DeleteTask()
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());
        foreach (Task task in tasks)
        {
            if (task.Id == id)
            {
                tasks.Remove(task);
                Console.WriteLine("Task is deleted");
                return;
            }
        }
        Console.WriteLine("Task is not found");
    }


    static void SearchByStatus()
    {
        Console.WriteLine("Enter the status i.e Pending or Completed");
        string Status = Console.ReadLine();

        bool searchFound = false;

        foreach (Task task in tasks)
        {

            if (task.Status.Equals(Status))
            {
                Console.WriteLine("ID: " + task.Id);
                Console.WriteLine("Description: " + task.Description);
                Console.WriteLine("Status: " + task.Status);
                Console.WriteLine();

                searchFound = true;
            }

        }
        if (!searchFound)
        {
            Console.WriteLine("No task found with provided status: " + Status);
        }
    }


    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Add");
            Console.WriteLine("2. List");
            Console.WriteLine("3. Complete");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Search By Status");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice:");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AddTask();
                    break;
                case 2:
                    ListTasks();
                    break;
                case 3:
                    CompleteTask();
                    break;
                case 4:
                    DeleteTask();
                    break;
                case 5:
                    SearchByStatus();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
