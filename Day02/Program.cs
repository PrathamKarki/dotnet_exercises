public abstract class Entity
{
    public abstract int Id { get; set; }
}

class Task : Entity
{

    private static int nextId = 1;
     public override int Id { get; set; }

    public string Description;

     private string status;
     public string Status
    {
        get
        {
            return status;
        }

        set
        {
            if (value.ToLower() == "new" || value.ToLower() == "pending" || value.ToLower() == "completed")
            {
                status = value;
            }
            else
            {
                throw new Exception("Invalid status");
            }
        }
    }

    public Task(string description, string status)
    {
        Id = nextId++;
        Description = description;
        Status = status;


    }
}
class Program
{
    static List<Task> tasks = new List<Task>();
    static void AddTask()
    {

        Console.Write("Enter the Description of your task:");
        string description = Console.ReadLine();
        Task task = new Task(description, "New");
        tasks.Add(task);
        Console.WriteLine("Task is added");
    }
    static void ListTasks()
    {
        foreach (Task task in tasks)
        {
            Console.WriteLine("Task ID: " + task.Id);
            Console.WriteLine("Description: " + task.Description);
            Console.WriteLine("Status: " + task.Status);
            Console.WriteLine();
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



    static void UpdateTaskStatus()
    {
        Console.Write("Enter the ID:");
        int id = int.Parse(Console.ReadLine());
        foreach (Task task in tasks)
        {
            if (task.Id == id)
            {
                Console.Write("Enter status New, Pending or Completed");
                task.Status = Console.ReadLine();

                Console.WriteLine("Status Updated");
                return;
            }
            Console.WriteLine("Task not found.");

        }
    }


    static void SearchByStatus()
    {
        Console.Write("Enter the status i.e New, Pending or Completed");
        string status = Console.ReadLine();

        bool searchFound = false;

        foreach (Task task in tasks)
        {

            if (task.Status.ToLower() == status.ToLower())
            {
                Console.WriteLine("Task ID: " + task.Id);
                Console.WriteLine("Description: " + task.Description);
                Console.WriteLine("Status: " + task.Status);
                Console.WriteLine();

                searchFound = true;
            }

        }
        if (!searchFound)
        {
            Console.WriteLine("No task found with provided status: " + status);
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
            Console.WriteLine("6. Update Status");
            Console.WriteLine("7. Exit");
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
                    UpdateTaskStatus();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
