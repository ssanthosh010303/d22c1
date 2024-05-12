using EmployeeRequestTracker.Models;
using EmployeeRequestTracker.Repositories;
using EmployeeRequestTracker.Services;

namespace EmployeeRequestTracker;

class Program
{
    static async Task Main()
    {
        var dbContext = new RequestTrackerContext();

        var employeeRepository = new EmployeeRepository(dbContext);
        var feedbackRepository = new Repository<int, SolutionFeedback>(dbContext);
        var requestRepository = new Repository<int, Request>(dbContext);

        var employeeLogin = new EmployeeLogin(employeeRepository);
        var feedback = new Feedback(feedbackRepository);
        var request = new RequestService(requestRepository);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register Feedback");
            Console.WriteLine("3. Add Request");
            Console.WriteLine("4. Update Request");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await Login(employeeLogin);
                    break;
                case "2":
                    await AddFeedback(feedback);
                    break;
                case "3":
                    await AddRequest(request);
                    break;
                case "4":
                    await UpdateRequest(request);
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static async Task Login(EmployeeLogin employeeLogin)
    {
        Console.Write("Enter employee ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        Employee employee = new Employee { Id = id, Password = password };

        var result = await employeeLogin.Login(employee);

        if (result != null)
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Invalid credentials, please try again.");
        }
    }

    static async Task AddFeedback(Feedback feedback)
    {
        Console.Write("Enter feedback: ");
        string feedbackText = Console.ReadLine();

        var solutionFeedback = new SolutionFeedback { Feedback = feedbackText };

        var result = await feedback.AddFeedback(solutionFeedback);

        Console.WriteLine("Feedback added successfully with ID: " + result.Id);
    }

    static async Task AddRequest(RequestService request)
    {
        Console.Write("Enter request description: ");
        string description = Console.ReadLine();

        Console.Write("Enter request raised by: ");
        int raisedBy = int.Parse(Console.ReadLine());

        var requestObj = new Request { Description = description, RequestRaisedBy = raisedBy };
        var result = await request.AddRequest(requestObj);

        Console.WriteLine("Request added successfully with number: " + result.RequestNumber);
    }

    static async Task UpdateRequest(RequestService request)
    {
        Console.Write("Enter request number: ");
        int requestNumber = int.Parse(Console.ReadLine());

        Console.Write("Enter new request status: ");
        string status = Console.ReadLine();

        var requestObj = await request.GetRequest(requestNumber);

        if (requestObj != null)
        {
            var updatedRequest = await request.UpdateRequest(requestObj, status);
            Console.WriteLine("Request updated successfully!");
        }
        else
        {
            Console.WriteLine("Request not found.");
        }
    }
}
