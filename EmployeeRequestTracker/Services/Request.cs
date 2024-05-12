using EmployeeRequestTracker.Models;
using EmployeeRequestTracker.Repositories;

namespace EmployeeRequestTracker.Services;

public class RequestService
{
    private readonly IRepository<int, Request> _requestRepository;

    public RequestService(IRepository<int, Request> requestRepository)
    {
        _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
    }

    public async Task<Request> AddRequest(Request request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return await _requestRepository.Add(request);
    }

    public async Task<Request> UpdateRequest(Request request, string requestStatus)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        request.RequestStatus = requestStatus;
        return await _requestRepository.Update(request);
    }

    public async Task<Request> GetRequest(int requestNumber)
    {
        return await _requestRepository.Get(requestNumber);
    }

    public async Task<IList<Request>> GetAllRequests()
    {
        return await _requestRepository.GetAll();
    }

    public async Task<IList<Request>> GetRequestwithID(int requestRaisedBy)
    {
        var allRequests = await _requestRepository.GetAll();
        return allRequests.Where(r => r.RequestRaisedBy == requestRaisedBy).ToList();
    }
}
