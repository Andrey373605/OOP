using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class EmployeeService : IEmployeeService
{
    IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<EmployeeRole> GetEmployeeRole(int userId, int bankId)
    {
        var employee = await _employeeRepository.GetEmployeeByUserIdAsync(bankId, userId);
        return employee.Role;
    }

    public async Task<Employee> GetEmployeeByUserIdAsync(int bankId, int userId)
    {
        var employee = await _employeeRepository.GetEmployeeByUserIdAsync(bankId, userId);
        if (employee == null)
        {
            throw new NullReferenceException("Employee not found");
        }
        return employee;
    }
}