using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IEmployeeRepository
{
    public Task AddAsync(Employee employee);
    public Task DeleteAsync(Employee employee);
    public Task<Employee> GetByIdAsync(int id);
    public Task UpdateAsync(Employee employee);

    public Task<Employee> GetEmployeeByUserIdAsync(int bankId, int userId);
    
    public Task<IEnumerable<Employee>> GetEmployeesAsync();
    public Task<IEnumerable<Employee>> GetEmployeeRequestsAsync();
    Task<Employee> GetEmployeeRequestAsync();
}