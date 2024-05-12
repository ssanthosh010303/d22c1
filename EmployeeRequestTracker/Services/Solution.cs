using EmployeeRequestTracker.Models;
using EmployeeRequestTracker.Repositories;

namespace EmployeeRequestTracker.Services;

public class SolutionService
{
    private readonly IRepository<int, RequestSolution> _solutionRepository;

    public SolutionService(IRepository<int, RequestSolution> solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }

    public async Task<RequestSolution> AddSolution(RequestSolution solution)
    {
        return await _solutionRepository.Add(solution);
    }

    public async Task<RequestSolution> GetSolution(int solutionId)
    {
        return await _solutionRepository.Get(solutionId);
    }

    public async Task<IList<RequestSolution>> GetAllSolutions()
    {
        return await _solutionRepository.GetAll();
    }

    public async Task<RequestSolution> UpdateSolution(RequestSolution solution)
    {
        return await _solutionRepository.Update(solution);
    }
}
