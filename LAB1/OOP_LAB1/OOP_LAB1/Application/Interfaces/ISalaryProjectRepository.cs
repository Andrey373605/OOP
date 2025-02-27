using System.Net.Sockets;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface ISalaryProjectRepository
{
    public Task AddAsync(SalaryProject salaryProject);
    
    public Task<SalaryProject> GetByIdAsync(int id);
    
    public Task UpdateAsync(SalaryProject salaryProject);

    public Task<IEnumerable<Account>> GetAccountInSalaryProjectAsync(SalaryProject salaryProject);
}