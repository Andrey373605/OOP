using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;


namespace OOP_LAB1.Application.Services;

public class SalaryProjectService : ISalaryProjectService
{
    ISalaryProjectRepository _salaryProjectRepository;

    public SalaryProjectService(ISalaryProjectRepository repository)
    {
        _salaryProjectRepository = repository;
    }

    public async Task CreateSalaryProjectApplication(int bankId, int enterpriseid)
    {
        SalaryProject project = new SalaryProject
        {
            BankId = bankId,
            EnterpriseId = enterpriseid
        };
        await _salaryProjectRepository.AddAsync(project);
    }


    public async Task ApproveSalaryProjectApplication(int id)
    {
        SalaryProject salaryProject = await _salaryProjectRepository.GetByIdAsync(id);
        salaryProject.SetActive();
        await _salaryProjectRepository.UpdateAsync(salaryProject);
    }

    public async Task AddAccountToSalaryProject(SalaryProject project, Account account)
    {
        throw new NotImplementedException();
    }

    public async Task PaySalary(SalaryProject salaryProject)
    {
        IEnumerable<Account> accounts = await _salaryProjectRepository.GetAccountInSalaryProjectAsync(salaryProject);
        
    }


}