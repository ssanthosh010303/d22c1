using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRequestTracker.Models;

public class SolutionFeedback
{
    [Key]
    public int FeedbackId { get; set; }

    public float Rating { get; set; }
    public string Remarks { get; set; }
    public int SolutionId { get; set; }

    // Navigation Property
    public virtual RequestSolution Solution { get; set; }
    public int FeedbackById { get; set; }

    // Navigation Property
    public virtual Employee FeedbackByEmployee { get; set; }
    public DateTime FeedbackDate { get; set; } = DateTime.Now;
}
