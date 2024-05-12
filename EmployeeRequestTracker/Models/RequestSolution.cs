using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRequestTracker.Models;

public class RequestSolution
{
    [Key]
    public int SolutionId { get; set; }

    // Foreign Key
    public int RequestId { get; set; }

    // Navigation Property
    public virtual Request RequestRaised { get; set; }
    public string SolutionDescription { get; set; }
    public int SolvedById { get; set; }

    // Navigation Property
    public virtual Employee SolvedByEmployee { get; set; }
    public DateTime SolvedDate { get; set; }
    public bool IsSolved { get; set; } = false;
    public string RequestRaiserComment { get; set; }
    public virtual ICollection<SolutionFeedback> Feedbacks { get; set; } = new List<SolutionFeedback>();
}
