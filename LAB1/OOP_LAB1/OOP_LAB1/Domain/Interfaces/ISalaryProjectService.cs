using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Domain.Interfaces;

public interface ISalaryProjectService
{
    public Task CreateSalaryProjectApplication(int bankId, int enterpriseid, int enterpriseAccountId);
    public Task ApproveSalaryProjectApplication(int id);
    public Task AddAccountToSalaryProject(int projectId, int accountId);
    
    public Task PaySalary(int projectId);
    
    public Task UpdateUserSalaryAmount(int projectId, int accountId, int amount);

    public Task DepositProjectAccount(int projectId, decimal amount);
}