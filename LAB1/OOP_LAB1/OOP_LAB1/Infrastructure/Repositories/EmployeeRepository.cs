using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public Task AddAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }
}