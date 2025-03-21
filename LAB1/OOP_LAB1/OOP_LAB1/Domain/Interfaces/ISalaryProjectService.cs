using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Domain.Interfaces;

public interface ISalaryProjectService
{
    public Task CreateSalaryProjectApplication(int enterpriseId);
    public Task ApproveSalaryProjectApplication(int id);
    public Task RejectSalaryProjectApplication(int id);
    
    public Task CreateSalaryApplication(int projectId, int accountId, decimal salary);
    public Task ApproveSalaryApplication(int salaryId);
    public Task RejectSalaryApplication(int salaryId);
    
    public Task PaySalary(int projectId);
    public Task UpdateSalaryAmount(int salaryId, decimal amount);
    public Task DepositProjectAccount(int fromAccountId,int projectId, decimal amount);
    public Task<IEnumerable<SalaryProject>> GetAllSalaryProjects();
    
    public Task<SalaryProject> GetSalaryProject(int id);
    public Task<IEnumerable<Salary>> GetAllSalaryRequests();
}