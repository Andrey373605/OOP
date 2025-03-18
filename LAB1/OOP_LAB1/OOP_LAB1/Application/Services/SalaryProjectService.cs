using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;


namespace OOP_LAB1.Application.Services;

public class SalaryProjectService : ISalaryProjectService
{
    private readonly ISalaryProjectRepository _salaryProjectRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IAccountRepository _accountRepository;

    public SalaryProjectService(ISalaryProjectRepository repository, IBankRepository bankRepository,
        IEnterpriseRepository enterpriseRepository, IAccountRepository accountRepository)
    {
        _salaryProjectRepository = repository;
        _bankRepository = bankRepository;
        _enterpriseRepository = enterpriseRepository;
        _accountRepository = accountRepository;
            
    }

    public async Task CreateSalaryProjectApplication(int bankId, int enterpriseId)
    {
        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new NullReferenceException("Bank could not be found");
        }
        
        var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseId);
        if (enterprise == null)
        {
            throw new NullReferenceException("Enterprise could not be found");
        }
        
        
        SalaryProject project = new SalaryProject
        {
            BankId = bank.Id,
            EnterpriseId = enterprise.Id,
            Status = SalaryProjectStatus.Application,
            Balance = 0
        };
        await _salaryProjectRepository.AddAsync(project);
    }
    

    public async Task ApproveSalaryProjectApplication(int id)
    {
        SalaryProject salaryProject = await _salaryProjectRepository.GetByIdAsync(id);
        
        salaryProject.Activate();
        await _salaryProjectRepository.UpdateAsync(salaryProject);
    }
    
    public async Task RejectSalaryProjectApplication(int id)
    {
        SalaryProject salaryProject = await _salaryProjectRepository.GetByIdAsync(id);
        
        salaryProject.Reject();
        await _salaryProjectRepository.UpdateAsync(salaryProject);
    }

    public async Task AddAccountToSalaryProject(int projectId, int accountId)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var account = await _accountRepository.GetByIdAsync(accountId);
        account.AccountType = AccountType.Salary;
        await _salaryProjectRepository.AddAccountToSalaryProjectAsync(project, account);
    }

    public async Task PaySalary(int projectId)
    {
        
    }

    public Task UpdateSalaryAmount(int projectId, int accountId, int amount)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserSalaryAmount(int projectId, int accountId, int amount)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var account = await _accountRepository.GetByIdAsync(accountId);
        await _salaryProjectRepository.UpdateSalaryAsync(project, account, amount);
    }

    public async Task DepositProjectAccount(int fromAccountId, int projectId, decimal amount)
    {
        
    }
}