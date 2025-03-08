using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;


namespace OOP_LAB1.Application.Services;

public class SalaryProjectService : ISalaryProjectService
{
    IAccountEnterpriseRepository _accountRepository;
    ISalaryProjectRepository _salaryProjectRepository;
    IBankRepository _bankRepository;
    IEnterpriseRepository _enterpriseRepository;

    public SalaryProjectService(ISalaryProjectRepository repository, IAccountEnterpriseRepository accountRepository, 
                                IBankRepository bankRepository, IEnterpriseRepository enterpriseRepository)
    {
        _salaryProjectRepository = repository;
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task CreateSalaryProjectApplication(int bankId, int enterpriseid)
    {
        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new NullReferenceException("Bank could not be found");
        }
        var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseid);
        if (enterprise == null)
        {
            throw new NullReferenceException("Enterprise could not be found");
        }
        
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
        await _salaryProjectRepository.AddAccountToSalaryProjectAsync(project, account);
    }

    public async Task PaySalary(int projectId)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var salaries = await _salaryProjectRepository.GetSalaryAmounts(project);
        var projectAccount = await _accountRepository.GetByIdAsync(project.AccountId);
        foreach (var s in salaries)
        {
            s.Key.DepositAccount(s.Value);
            projectAccount.WithdrawAccount(s.Value);
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

    public async Task DepositProjectAccount(int projectId, decimal amount)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var account = await _accountRepository.GetByIdAsync(project.AccountId);
        account.DepositAccount(amount);
        await _accountRepository.UpdateAsync(account);
    }
}