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

    public async Task CreateSalaryApplication(int projectId, int accountId, decimal amount)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        var account = await _accountRepository.GetByIdAsync(accountId);

        if (project.Status != SalaryProjectStatus.Active)
        {
            throw new ApplicationException("project is not active");
        }

        var salary = new Salary
        {
            AccountId = account.Id,
            Amount = amount,
            Status = SalaryStatus.Application,
            SalaryProjectId = projectId,
        };
        
        await _salaryProjectRepository.AddSalaryAsync(salary);
    }

    public async Task ApproveSalaryApplication(int salaryId)
    {
        var salary = await _salaryProjectRepository.GetSalaryAsync(salaryId);
        if (salary.Status != SalaryStatus.Application)
        {
            throw new ApplicationException("Its not a request to approve salary");
        }
        salary.Activate();
        await _salaryProjectRepository.UpdateSalaryAsync(salary);
    }

    public async Task RejectSalaryApplication(int salaryId)
    {
        var salary = await _salaryProjectRepository.GetSalaryAsync(salaryId);
        if (salary.Status != SalaryStatus.Application)
        {
            throw new ApplicationException("Its not a request to reject salary");
        }
        salary.Reject();
        await _salaryProjectRepository.UpdateSalaryAsync(salary);
    }
    
    public async Task PaySalary(int projectId)
    {
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);
        if (project.Status == SalaryProjectStatus.Blocked)
        {
            throw new ApplicationException("Project is blocked");
        }
        
        var salaries = await _salaryProjectRepository.GetSalaries(projectId);
        foreach (var s in salaries)
        {
            var account = await _accountRepository.GetByIdAsync(s.AccountId);
            if (s.Status == SalaryStatus.Active)
            {
                account.DepositAccount(s.Amount);
                project.Withdraw(s.Amount);
                await _accountRepository.UpdateAsync(account);
            }
            
        }

        if (project.Balance < 0)
        {
            project.Block();
        }
        
        await _salaryProjectRepository.UpdateAsync(project);
        
    }


    public async Task UpdateSalaryAmount(int salaryId, int amount)
    {
        
        var salary = await _salaryProjectRepository.GetSalaryAsync(salaryId);
        salary.Amount = amount;
        await _salaryProjectRepository.UpdateSalaryAsync(salary);
    }

    public async Task DepositProjectAccount(int fromAccountId, int projectId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(fromAccountId);
        if (account.Status != AccountStatus.Active)
        {
            throw new ArgumentException("Account not active");
        }

        if (account.Balance < amount)
        {
            throw new NullReferenceException("Insufficient balance");
        }
        
        var project = await _salaryProjectRepository.GetByIdAsync(projectId);

        if (project.Status != SalaryProjectStatus.Active)
        {
            throw new ArgumentException("Project not active");
        }
        
        project.Deposit(amount);
        account.WithdrawAccount(amount);

        if (project is { Balance: > 0, Status: SalaryProjectStatus.Blocked })
        {
            project.Unblock();
        }
        
        await _salaryProjectRepository.UpdateAsync(project);
        await _accountRepository.UpdateAsync(account);
    }

    public async Task<IEnumerable<SalaryProject>> GetAllSalaryProjects()
    {
        return await _salaryProjectRepository.GetAllSalaryProjectAsync();
    }

    public async Task<SalaryProject> GetSalaryProject(int id)
    {
        return await _salaryProjectRepository.GetByIdAsync(id);
    }
}