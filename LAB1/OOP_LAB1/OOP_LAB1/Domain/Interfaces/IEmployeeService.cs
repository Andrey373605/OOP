using System.Collections;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeRole> GetEmployeeRole(int userId, int bankId);
    Task<Employee> GetEmployeeByUserIdAsync(int bankId, int userId);
    Task<IEnumerable<Employee>> GetClientRegistrationRequests();
    Task ApproveClientRegistration(int id);
    Task RejectClientRegistration(int id);
}