using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using EmployeeRequestTracker.Models;

namespace EmployeeRequestTracker.Repositories;

public class RequestTrackerContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<RequestTrackerContext>()
                .Build();
            var connectionString = builder.GetConnectionString("DevelopmentDatabase");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<RequestSolution> RequestSolutions { get; set; }
    public DbSet<SolutionFeedback> SolutionFeedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 101, Name = "Sakthi", Password = "pass", Role = "Admin" },
            new Employee { Id = 102, Name = "Santhosh", Password = "pass", Role = "Admin" },
            new Employee { Id = 103, Name = "Bimu", Password = "pass", Role = "User" }
        );

        modelBuilder.Entity<Request>().HasKey(r => r.RequestNumber);
        modelBuilder.Entity<SolutionFeedback>().HasKey(r => r.FeedbackId);

        modelBuilder.Entity<Request>()
            .HasOne(r => r.RaisedByEmployee)
            .WithMany(e => e.RequestsRaised)
            .HasForeignKey(r => r.RequestRaisedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<Request>()
            .HasOne(r => r.RequestClosedByEmployee)
            .WithMany(e => e.RequestsClosed)
            .HasForeignKey(r => r.RequestClosedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestSolution>()
            .HasOne(rs => rs.RequestRaised)
            .WithMany(r => r.RequestSolutions)
            .HasForeignKey(rs => rs.RequestId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<RequestSolution>()
            .HasOne(rs => rs.SolvedByEmployee)
            .WithMany(e => e.SolutionsProvided)
            .HasForeignKey(rs => rs.SolvedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<SolutionFeedback>()
            .HasOne(sf => sf.Solution)
            .WithMany(s => s.Feedbacks)
            .HasForeignKey(sf => sf.SolutionId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<SolutionFeedback>()
            .HasOne(sf => sf.FeedbackByEmployee)
            .WithMany(e => e.FeedbacksGiven)
            .HasForeignKey(sf => sf.FeedbackById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
