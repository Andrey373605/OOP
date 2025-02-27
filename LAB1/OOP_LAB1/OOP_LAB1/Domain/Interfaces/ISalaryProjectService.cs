using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Domain.Interfaces;

public interface ISalaryProjectService
{
    public Task CreateSalaryProjectApplication(int bankId, int enterpriseid);
    public Task ApproveSalaryProjectApplication(int id);
    public Task AddAccountToSalaryProject(SalaryProject project, Account account);
    public Task PaySalary(SalaryProject project);
}