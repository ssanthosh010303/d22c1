using EmployeeRequestTracker.Models;
using EmployeeRequestTracker.Repositories;

namespace EmployeeRequestTracker.Services;

public class EmployeeLoginService
{
    private readonly IRepository<int, Employee> _repository;

    public EmployeeLoginService(IRepository<int, Employee> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Employee?> Login(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        var emp = await _repository.Get(employee.Id);

        if (emp != null && emp.Password == employee.Password)
            return emp;

        return null;
    }

    public async Task<Employee> Register(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        var result = await _repository.Add(employee);
        return result;
    }
}
