using EmployeeRequestTracker.Models;
using EmployeeRequestTracker.Repositories;

namespace EmployeeRequestTracker.Services;

public class FeedbackService
{
    private readonly IRepository<int, SolutionFeedback> _feedbackRepository;

    public FeedbackService(IRepository<int, SolutionFeedback> feedbackRepository)
    {
        _feedbackRepository = feedbackRepository ?? throw new ArgumentNullException(nameof(feedbackRepository));
    }

    public async Task<SolutionFeedback> AddFeedback(SolutionFeedback feedback)
    {
        if (feedback == null)
            throw new ArgumentNullException(nameof(feedback));

        return await _feedbackRepository.Add(feedback);
    }

    public async Task<SolutionFeedback> GetFeedback(int feedbackId)
    {
        return await _feedbackRepository.Get(feedbackId);
    }

    public async Task<IList<SolutionFeedback>> GetAllFeedbacks()
    {
        return await _feedbackRepository.GetAll();
    }
}
