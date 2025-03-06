using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;


namespace OOP_LAB1.Application.Services;

public class SalaryProjectService : ISalaryProjectService
{
    IAccountRepository _accountRepository;
    ISalaryProjectRepository _salaryProjectRepository;

    public SalaryProjectService(ISalaryProjectRepository repository, IAccountRepository accountRepository)
    {
        _salaryProjectRepository = repository;
        _accountRepository = accountRepository;
    }

    public async Task CreateSalaryProjectApplication(int bankId, int enterpriseid)
    {
        SalaryProject project = new SalaryProject
        {
            BankId = bankId,
            EnterpriseId = enterpriseid,
            IsActive = false,
        };
        await _salaryProjectRepository.AddAsync(project);
    }


    public async Task ApproveSalaryProjectApplication(int id)
    {
        SalaryProject salaryProject = await _salaryProjectRepository.GetByIdAsync(id);
        salaryProject.SetActive();
        await _salaryProjectRepository.UpdateAsync(salaryProject);
    }

    public async Task AddAccountToSalaryProject(int projectId, int accountId)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var account = await _accountRepository.GetByIdAsync(accountId);
        _salaryProjectRepository.AddAccountToSalaryProjectAsync(project, account);
    }

    public async Task PaySalary(int projectId)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        IEnumerable<Account> accounts = await _salaryProjectRepository.GetAccountInSalaryProjectAsync(project);
        var salaries = await _salaryProjectRepository.GetSalaryAmounts(project);
        var projectAccount = await _accountRepository.GetByIdAsync(project.AccountId);
        foreach (var s in salaries)
        {
            s.Key.Balance += s.Value;
            projectAccount.Balance -= s.Value;
        }
        await _salaryProjectRepository.UpdateAsync(project);
        await _accountRepository.UpdateAsync(projectAccount);
    }

    public async Task UpdateUserSalaryAmount(int projectId, int accountId, int amount)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var account = await _accountRepository.GetByIdAsync(accountId);
        await _salaryProjectRepository.UpdateSalaryAsync(project, account, amount);
    }
}