using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class SalaryProjectService : ISalaryProjectService
{
    private readonly ISalaryProjectRepository _salaryProjectRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger _logger;

    public SalaryProjectService(ISalaryProjectRepository repository, IBankRepository bankRepository,
        IEnterpriseRepository enterpriseRepository, IAccountRepository accountRepository, ILogger logger)
    {
        _salaryProjectRepository = repository;
        _bankRepository = bankRepository;
        _enterpriseRepository = enterpriseRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task CreateSalaryProjectApplication(int enterpriseId)
    {
        try
        {
            _logger.Information($"Attempting to create salary project application for enterprise with ID: {enterpriseId}");
            var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseId);
            if (enterprise == null)
            {
                _logger.Error($"Enterprise with ID {enterpriseId} not found");
                throw new NullReferenceException("Enterprise could not be found");
            }
            var bank = await _bankRepository.GetByIdAsync(enterprise.BankId);
            if (bank == null)
            {
                _logger.Error($"Bank with ID {enterprise.BankId} not found");
                throw new NullReferenceException("Bank could not be found");
            }
            SalaryProject project = new SalaryProject
            {
                BankId = bank.Id,
                EnterpriseId = enterprise.Id,
                Status = SalaryProjectStatus.Application,
                Balance = 0
            };
            await _salaryProjectRepository.AddAsync(project);
            _logger.Information($"Successfully created salary project application for enterprise with ID {enterpriseId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error creating salary project application for enterprise with ID {enterpriseId}");
            throw;
        }
    }

    public async Task ApproveSalaryProjectApplication(int id)
    {
        try
        {
            _logger.Information($"Attempting to approve salary project application with ID: {id}");
            var salaryProject = await _salaryProjectRepository.GetByIdAsync(id);
            if (salaryProject == null)
            {
                _logger.Error($"Salary project with ID {id} not found");
                throw new NullReferenceException("Salary project could not be found");
            }
            salaryProject.Activate();
            await _salaryProjectRepository.UpdateAsync(salaryProject);
            _logger.Information($"Successfully approved salary project application with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error approving salary project application with ID {id}");
            throw;
        }
    }

    public async Task RejectSalaryProjectApplication(int id)
    {
        try
        {
            _logger.Information($"Attempting to reject salary project application with ID: {id}");
            var salaryProject = await _salaryProjectRepository.GetByIdAsync(id);
            if (salaryProject == null)
            {
                _logger.Error($"Salary project with ID {id} not found");
                throw new NullReferenceException("Salary project could not be found");
            }
            salaryProject.Reject();
            await _salaryProjectRepository.UpdateAsync(salaryProject);
            _logger.Information($"Successfully rejected salary project application with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error rejecting salary project application with ID {id}");
            throw;
        }
    }

    public async Task CreateSalaryApplication(int projectId, int accountId, decimal amount)
    {
        try
        {
            _logger.Information($"Attempting to create salary application for project ID: {projectId} and account ID: {accountId}");
            var project = await _salaryProjectRepository.GetByIdAsync(projectId);
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (project.Status != SalaryProjectStatus.Active)
            {
                _logger.Error($"Salary project with ID {projectId} is not active");
                throw new ApplicationException("Project is not active");
            }
            var salary = new Salary
            {
                AccountId = account.Id,
                Amount = amount,
                Status = SalaryStatus.Application,
                SalaryProjectId = projectId,
            };
            await _salaryProjectRepository.AddSalaryAsync(salary);
            _logger.Information($"Successfully created salary application for project ID {projectId} and account ID {accountId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error creating salary application for project ID {projectId} and account ID {accountId}");
            throw;
        }
    }

    public async Task ApproveSalaryApplication(int salaryId)
    {
        try
        {
            _logger.Information($"Attempting to approve salary application with ID: {salaryId}");
            var salary = await _salaryProjectRepository.GetSalaryRequest(salaryId);
            if (salary.Status != SalaryStatus.Application)
            {
                _logger.Error($"Salary application with ID {salaryId} is not in application status");
                throw new ApplicationException("It's not a request to approve salary");
            }
            salary.Activate();
            await _salaryProjectRepository.UpdateSalaryAsync(salary);
            _logger.Information($"Successfully approved salary application with ID {salaryId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error approving salary application with ID {salaryId}");
            throw;
        }
    }

    public async Task RejectSalaryApplication(int salaryId)
    {
        try
        {
            _logger.Information($"Attempting to reject salary application with ID: {salaryId}");
            var salary = await _salaryProjectRepository.GetSalaryRequest(salaryId);
            if (salary.Status != SalaryStatus.Application)
            {
                _logger.Error($"Salary application with ID {salaryId} is not in application status");
                throw new ApplicationException("It's not a request to reject salary");
            }
            salary.Reject();
            await _salaryProjectRepository.UpdateSalaryAsync(salary);
            _logger.Information($"Successfully rejected salary application with ID {salaryId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error rejecting salary application with ID {salaryId}");
            throw;
        }
    }

    public async Task PaySalary(int projectId)
    {
        try
        {
            _logger.Information($"Attempting to pay salary for project ID: {projectId}");
            var project = await _salaryProjectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                _logger.Error($"Salary project with ID {projectId} not found");
                throw new NullReferenceException("Salary project could not be found");
            }
            if (project.Status == SalaryProjectStatus.Blocked)
            {
                _logger.Error($"Salary project with ID {projectId} is blocked");
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
            _logger.Information($"Successfully paid salary for project ID {projectId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error paying salary for project ID {projectId}");
            throw;
        }
    }
    
    public async Task UpdateSalaryAmount(int salaryId, decimal amount)
    {
        try
        {
            _logger.Information($"Attempting to update salary amount for salary ID: {salaryId} to {amount}");
            var salary = await _salaryProjectRepository.GetSalaryAsync(salaryId);
            if (salary == null)
            {
                _logger.Error($"Salary with ID {salaryId} not found");
                throw new NullReferenceException("Salary could not be found");
            }
            salary.Amount = amount;
            await _salaryProjectRepository.UpdateSalaryAsync(salary);
            _logger.Information($"Successfully updated salary amount for salary ID {salaryId} to {amount}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error updating salary amount for salary ID {salaryId} to {amount}");
            throw;
        }
    }

    public async Task DepositProjectAccount(int fromAccountId, int projectId, decimal amount)
    {
        try
        {
            _logger.Information($"Attempting to deposit {amount} into project account for project ID: {projectId} from account ID: {fromAccountId}");
            var account = await _accountRepository.GetByIdAsync(fromAccountId);
            if (account.Status != AccountStatus.Active)
            {
                _logger.Error($"Account with ID {fromAccountId} is not active");
                throw new ArgumentException("Account not active");
            }
            if (account.Balance < amount)
            {
                _logger.Error($"Insufficient balance in account with ID {fromAccountId}");
                throw new NullReferenceException("Insufficient balance");
            }
            var project = await _salaryProjectRepository.GetByIdAsync(projectId);
            if (project.Status != SalaryProjectStatus.Active)
            {
                _logger.Error($"Salary project with ID {projectId} is not active");
                throw new ArgumentException("Project not active");
            }
            project.Deposit(amount);
            account.WithdrawAccount(amount);
            if (project.Balance > 0 && project.Status == SalaryProjectStatus.Blocked)
            {
                project.Unblock();
            }
            await _salaryProjectRepository.UpdateAsync(project);
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Successfully deposited {amount} into project account for project ID {projectId} from account ID {fromAccountId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error depositing {amount} into project account for project ID {projectId} from account ID {fromAccountId}");
            throw;
        }
    }

    public async Task<IEnumerable<SalaryProject>> GetAllSalaryProjects()
    {
        try
        {
            _logger.Information("Attempting to retrieve all salary projects");
            var projects = await _salaryProjectRepository.GetAllSalaryProjectAsync();
            _logger.Information("Successfully retrieved all salary projects");
            return projects;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all salary projects");
            throw;
        }
    }

    public async Task<SalaryProject> GetSalaryProject(int id)
    {
        try
        {
            _logger.Information($"Attempting to retrieve salary project with ID: {id}");
            var project = await _salaryProjectRepository.GetByIdAsync(id);
            if (project == null)
            {
                _logger.Error($"Salary project with ID {id} not found");
                throw new NullReferenceException("Salary project could not be found");
            }
            _logger.Information($"Successfully retrieved salary project with ID {id}");
            return project;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving salary project with ID {id}");
            throw;
        }
    }

    public async Task<IEnumerable<Salary>> GetAllSalaryRequests()
    {
        try
        {
            _logger.Information("Attempting to retrieve all salary requests");
            var requests = await _salaryProjectRepository.GetAllSalaryRequests();
            _logger.Information("Successfully retrieved all salary requests");
            return requests;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all salary requests");
            throw;
        }
    }

    public async Task<IEnumerable<SalaryProject>> GetAllSalaryProjectRequests()
    {
        try
        {
            _logger.Information("Attempting to retrieve all salary prject requests");
            var requests = await _salaryProjectRepository.GetAllSalaryProjectRequests();
            _logger.Information("Successfully retrieved all salary prject requests");
            return requests;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all salary prject requests");
            throw;
        }
    }
}