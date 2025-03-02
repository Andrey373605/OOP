using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public Task<Employee> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Employee employee)
    {
        throw new NotImplementedException();
    }
}