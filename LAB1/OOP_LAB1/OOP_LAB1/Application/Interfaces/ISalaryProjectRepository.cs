using System.Net.Sockets;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface ISalaryProjectRepository
{
    public Task AddAsync(SalaryProject salaryProject);
    
    public Task<SalaryProject> GetByIdAsync(int id);
    
    public Task UpdateAsync(SalaryProject salaryProject);

    public Task AddAccountToSalaryProjectAsync(SalaryProject project, Account account, decimal salary);
    public Task UpdateSalaryAsync(SalaryProject project, Account account, decimal amount);
    public Task<IEnumerable<Salary>> GetSalaries(int projectId);
}