using System.Net.Sockets;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface ISalaryProjectRepository
{
    public Task AddAsync(SalaryProject salaryProject);
    
    public Task<SalaryProject> GetByIdAsync(int id);
    
    public Task UpdateAsync(SalaryProject salaryProject);

    public Task<IEnumerable<Account>> GetAccountInSalaryProjectAsync(SalaryProject salaryProject);
    public Task AddAccountToSalaryProjectAsync(SalaryProject project, Account account);
    public Task<Dictionary<Account, decimal>> GetSalaryAmounts(SalaryProject project);
    public Task UpdateSalaryAsync(SalaryProject project, Account account, int amount);
}