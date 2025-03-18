using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Domain.Interfaces;

public interface ISalaryProjectService
{
    public Task CreateSalaryProjectApplication(int bankId, int enterpriseId);
    public Task ApproveSalaryProjectApplication(int id);
    
    public Task RejectSalaryProjectApplication(int id);
    public Task AddAccountToSalaryProject(int projectId, int accountId);
    
    public Task PaySalary(int projectId);
    
    public Task UpdateSalaryAmount(int projectId, int accountId, int amount);

    public Task DepositProjectAccount(int fromAccountId,int projectId, decimal amount);
}