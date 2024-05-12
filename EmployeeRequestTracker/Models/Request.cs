using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRequestTracker.Models;

public class Request
{
    [Key]
    public int RequestNumber { get; set; }

    public string RequestMessage { get; set; }

    public DateTime RequestDate { get; set; } = DateTime.Now;
    public DateTime? ClosedDate { get; set; }
    public string RequestStatus { get; set; }

    // Foreign Key
    public int? RequestRaisedById { get; set; }
    public int? RequestClosedById { get; set; }

    // Navigation Properties
    public virtual Employee RaisedByEmployee { get; set; }
    public virtual Employee RequestClosedByEmployee { get; set; }
    public virtual ICollection<RequestSolution> RequestSolutions { get; set; } = new List<RequestSolution>();
}
