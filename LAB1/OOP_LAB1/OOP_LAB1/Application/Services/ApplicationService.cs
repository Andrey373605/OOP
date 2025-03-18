using System.Collections;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IContext _context;
    private readonly IAuthorizationService _authorizationService;
    private readonly IAccountService _accountService;
    private readonly IBankRepository _bankRepository;
    private readonly ITransactionService _transactionService;
    private readonly IInstallmentService _installmentService;
    private readonly ILoanService _loanService;
    private readonly IEmployeeService _employeeService;
    private readonly IClientService _clientService;


    public ApplicationService(IContext context, IAuthorizationService authorizationService, 
        IAccountService accountService, IBankRepository bankRepository,
        ITransactionService transactionService, IInstallmentService installmentService, ILoanService loanService,
        IClientService clientService, IEmployeeService employeeService)
    {
        _context = context;
        _authorizationService = authorizationService;
        _accountService = accountService;
        _bankRepository = bankRepository;
        _transactionService = transactionService;
        _installmentService = installmentService;
        _loanService = loanService;
        _clientService = clientService;
        _employeeService = employeeService;
    }
    
    public async Task LoginUser(string email, string password)
    {
       var user = await _authorizationService.AuthenticateUserAsync(email, password);
       _context.SetCurrent(user);
    }

    public async Task<IEnumerable<Account>> GetCurrentClientAccounts()
    {
        var client = await GetCurrentClient();
        if (client == null)
        {
            return new List<Account>();
        }
        
        var accounts = await _accountService.GetAllClientAccountsAsync(client.Id);
        return accounts.ToList();
    }

    public async Task<bool> IsCurrentUserClient()
    {
        try
        {
            var client = await GetCurrentClient();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }

    public async Task<Client> GetCurrentClient()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User null");
        }

        if (bank == null)
        {
            throw new UnauthorizedAccessException("Bank null");
        }
        var client = await _clientService.GetClientByUserIdAsync(bank.Id, user.Id);
        if (client == null)
        {
            throw new UnauthorizedAccessException("Client null");
        }
        return client;
    }
    
    public async Task<Employee> GetCurrentEmployee()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User null");
        }

        if (bank == null)
        {
            throw new UnauthorizedAccessException("Bank null");
        }
        var employee = await _employeeService.GetEmployeeByUserIdAsync(user.Id, bank.Id);
        if (employee == null)
        {
            throw new UnauthorizedAccessException("Employee null");
        }

        return employee;
    }

    public void LoginBank(Bank bank)
    {
        _context.SetCurrent(bank);
    }

    public async Task LoginClient()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        await _authorizationService.AuthenticateClientAsync(user.Id, bank.Id);
    }

    public async Task RegisterClient(string firstName, string lastName, string middleName, string phoneNumber,
        string identificationNumber, string series)
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        await _authorizationService.RegisterClientAsync(user.Id, bank.Id, firstName, lastName, middleName, phoneNumber, identificationNumber, series);
    }

    public async Task RegisterEmployee(EmployeeRole role)
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        await _authorizationService.RegisterEmployeeAsync(user.Id, bank.Id, role);
    }

    public async Task CreateAccount()
    {
        var client = await GetCurrentClient();
        await _accountService.CreateAccountAsync(client.Id);
    }

    public async Task DepositAccount(int accountId, decimal sum)
    {
        var client = await GetCurrentClient();
        var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        if (check == false)
        {
            throw new ApplicationException("Account is not belong to client");
        }
        await _transactionService.DepositFunds(sum, accountId);
    }

    public async Task TransferAccount(int fromAccountId, int toAccountId, decimal sum)
    {
        var client = await GetCurrentClient();
        var check = await _accountService.IsAccountBelongToClient(fromAccountId, client.Id);
        if (check == false)
        {
            throw new ApplicationException("From account is not belong to client");
        }

        if (fromAccountId == toAccountId)
        {
            throw new ApplicationException("From account id can't be the same");
        }
        await _transactionService.TransferFunds(sum, fromAccountId, toAccountId);
    }

    public async Task FreezeAccount(int accountId)
    {
        var client = await GetCurrentClient();
        var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        if (check == false)
        {
            throw new ApplicationException("You can not freeze account that doesn't belong to you");
        }
        await _accountService.FreezeAccountAsync(accountId);
    }
    
    public async Task UnfreezeAccount(int accountId)
    {
        var client = await GetCurrentClient();
        var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        if (check == false)
        {
            throw new ApplicationException("You can not unfreeze account that doesn't belong to you");
        }
        await _accountService.UnfreezeAccountAsync(accountId);
    }

    public async Task CreateInstallmentRequest(decimal sum, int duration)
    {
        var client = await GetCurrentClient();
        await _installmentService.CreateInstallmentRequest(client.Id, sum, duration);
    }

    public async Task CreateLoanRequest(decimal sum, int rate, int duration)
    {
        var client = await GetCurrentClient();
        await _loanService.CreateLoanRequest(client.Id, sum, rate, duration);
    }

    public async Task LoginEmployee()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        await _authorizationService.AuthenticateEmployeeAsync(user.Id, bank.Id);
    }

    public async Task<EmployeeRole> GetCurrentEmployeeRole()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        return await _employeeService.GetEmployeeRole(user.Id, bank.Id);
    }

    public async Task<IEnumerable<Loan>> GetCurrentClientLoans()
    {
        var client = await GetCurrentClient();
        if (client == null)
        {
            return new List<Loan>();
        }
        
        var accounts = await _loanService.GetAllClientLoansAsync(client.Id);
        return accounts.ToList();
    }

    public async Task<IEnumerable<Installment>> GetCurrentClientInstallments()
    {
        var client = await GetCurrentClient();
        if (client == null)
        {
            return new List<Installment>();
        }
        
        var accounts = await _installmentService.GetAllClientInstallmentsAsync(client.Id);
        return accounts.ToList();
    }

    public async Task<IEnumerable<Transaction>> GetTransfersByAccountIdAsync(int accountId)
    {
        var checkClient = await IsCurrentUserClient();
        if (checkClient)
        {
            var client = await GetCurrentClient();
            var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        
            if (!check)
            {
                throw new ApplicationException("Account is not belong to client");
            }
        }
        
        return await _transactionService.GetTransferByAccountId(accountId);
    }

    public async Task<IEnumerable<Transaction>> GetDepositsByAccountIdAsync(int accountId)
    {
        var checkClient = await IsCurrentUserClient();
        if (checkClient)
        {
            var client = await GetCurrentClient();
            var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        
            if (!check)
            {
                throw new ApplicationException("Account is not belong to client");
            }
        }
        
        return await _transactionService.GetDepositByAccountId(accountId);
    }

    public async Task<IEnumerable<Transaction>> GetWithdrawsByAccountIdAsync(int accountId)
    {
        var checkClient = await IsCurrentUserClient();
        if (checkClient)
        {
            var client = await GetCurrentClient();
            var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        
            if (!check)
            {
                throw new ApplicationException("Account is not belong to client");
            }
        }
        
        return await _transactionService.GetWithdrawByAccountId(accountId);
    }

    public async Task LogOutUser()
    {
        _context.ClearCurrentUser();
        _context.ClearCurrentBank();
    }

    public async Task CancelTransfer(int numberTransfer)
    {
        var transaction = await _transactionService.GetTransferById(numberTransfer);
        if (transaction == null)
        {
            throw new ApplicationException("Invalid number transfer");
        }
        await _transactionService.TransferFunds(transaction.Amount, transaction.FromAccountId??0, transaction.ToAccountId??0);
    }

    public async Task WithdrawAccount(int accountId, decimal sum)
    {
        var client = await GetCurrentClient();
        var check = await _accountService.IsAccountBelongToClient(accountId, client.Id);
        if (check == false)
        {
            throw new ApplicationException("From account is not belong to client");
        }
        await _transactionService.WithdrawFunds(sum, accountId);
    }
}