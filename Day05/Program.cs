using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

public abstract class Entity
{
    public abstract int Id { get; set; }
}

class Task : Entity, IComparable<Task>
{

    private static int nextId = 1;
    public override int Id { get; set; }

    public string Description;

    public DateOnly EffectiveDate { get; set; }
    public DateTime CreatedDate { get;  private set; }
    public DateTime UpdatedDate { get; set; }


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

    public int Priority { get; set; }




    public Task(string description, string status, int priority, DateOnly effectiveDate)
    {
        Id = nextId++;
        Description = description;
        Status = status;
        Priority = priority;
        EffectiveDate = effectiveDate;
        CreatedDate = DateTime.Now;
        UpdatedDate = DateTime.Now;

    }

    public int CompareTo(Task other)
    {
        if (other == null)
        {
            return 1;
        }
        return Priority.CompareTo(other.Priority);
    }


}
class Program
{
    static List<Task> tasks = new List<Task>();
    static Queue<Task> reviewQueue = new Queue<Task>();

    // regex patter for the validation

    private static readonly string DatePattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";
   
    static void AddTask()
    {

        Console.Write("Enter the Description of your task:");
        string description = Console.ReadLine();

        Console.Write("Enter the priority level :");
        int priority = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the effective date");
        string dateinput = Console.ReadLine();


        string dateInput;
        DateOnly effectiveDate;
        while (true)
        {
            Console.Write("Enter effective date in YYYY MM DD format:");
            dateInput = Console.ReadLine();

            if (Regex.IsMatch(dateInput, DatePattern) && DateOnly.TryParse(dateInput, out effectiveDate)) {
                break;
            }
            Console.WriteLine("Invalid date, use the YYYY-MM-DD format");
        }


        Task task = new Task(description, "New", priority, effectiveDate);
        tasks.Add(task);
        Console.WriteLine("Task is added");
    }
    static void ListTasks()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        foreach (Task task in tasks)
        {
            bool isEffective = task.EffectiveDate <= today;
            string dataStatusTag = isEffective ? "due now" : "upcomming";

            Console.WriteLine("Task ID: " + task.Id);
            Console.WriteLine("Priority: " + task.Priority);
            Console.WriteLine("Description: " + task.Description);
            Console.WriteLine("Status: " + task.Status);
            Console.WriteLine("Effective date: "+ task.EffectiveDate);
            Console.WriteLine("Created Date: " + task.CreatedDate);
            Console.WriteLine("Updated Date: " + task.UpdatedDate);
        }
    }


    static void SortTaskByPriority()
    {
        tasks.Sort();
        Console.WriteLine("Tasks sorted by priority");
    }


    static void AddToReviewQueue()
    {
        Console.Write("Enter  Task ID to send for review:");
        int id = int.Parse(Console.ReadLine());
        Task task = tasks.Find(x => x.Id == id);

        if (task != null)
        {
            reviewQueue.Enqueue(task);
            Console.WriteLine("Task added to review queue");
        }
        else
        {
            Console.WriteLine("Task not found");
        }

    }

    static void ProcessReviewQueue()
    {
        if (reviewQueue.Count > 0)
        {
            Task task = reviewQueue.Dequeue();
            Console.WriteLine("Reviewed Task ID: " + task.Id);
            Console.WriteLine("Reviewed Task Description " + task.Description);

        }
        else
        {
            Console.WriteLine("Review queue is empty");
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
            Console.WriteLine("7. Sort task By Priority");
            Console.WriteLine("8. Add to Review Queue");
            Console.WriteLine("9. Process Review Queue");
            Console.WriteLine("10. Exit");
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
                    SortTaskByPriority();
                    break;
                case 8:
                    AddToReviewQueue();
                    break;
                case 9:
                    ProcessReviewQueue();
                    break;
                case 10:
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
