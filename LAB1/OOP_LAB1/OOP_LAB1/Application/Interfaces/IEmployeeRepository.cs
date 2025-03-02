using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface IEmployeeRepository
{
    public Task<Employee> GetByEmailAsync(string email);

    public Task AddAsync(Employee employee);
}