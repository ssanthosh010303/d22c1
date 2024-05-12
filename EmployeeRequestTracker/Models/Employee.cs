using System.Collections.Generic;

namespace EmployeeRequestTracker.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    // Navigation Properties
    public virtual ICollection<Request> RequestsRaised { get; set; } = new List<Request>();
    public virtual ICollection<Request> RequestsClosed { get; set; } = new List<Request>();
    public virtual ICollection<RequestSolution> SolutionsProvided { get; set; } = new List<RequestSolution>();
    public virtual ICollection<SolutionFeedback> FeedbacksGiven { get; set; } = new List<SolutionFeedback>();

    // ToString override
    public override string ToString()
    {
        return $"{Id} {Name} {Role}";
    }

    public virtual bool PasswordCheck(string password)
    {
        return Password == password;
    }
}
