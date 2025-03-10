using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class ApplicationService : IApplicationService
{
    IContext _context;
    IAuthorizationService _authorizationService;
    IAccountService _accountService;
    IBankRepository _bankRepository;

    public ApplicationService(IContext context, IAuthorizationService authorizationService, IAccountService accountService, IBankRepository bankRepository)
    {
        _context = context;
        _authorizationService = authorizationService;
        _accountService = accountService;
        _bankRepository = bankRepository;
    }
    
    public async Task LoginUser(string email, string password)
    {
       var user = await _authorizationService.AuthenticateUserAsync(email, password);
       _context.SetCurrent(user);
    }

    public async Task<IEnumerable<Account>> GetCurrentClientAccounts()
    {
        var client = await GetCurrentClient();
        
        var accounts = await _accountService.GetAllClientAccountsAsync(client.Id);
        return accounts;
    }

    public async Task<Client> GetCurrentClient()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        return await _bankRepository.GetClientByUserIdAsync(user.Id, bank.Id);
    }
    
    public async Task<Employee> GetCurrentEmployee()
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        return await _bankRepository.GetEmployeeByUserIdAsync(user.Id, bank.Id);
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

    public async Task RegisterEmployee(UserRole role)
    {
        var user = _context.CurrentUser;
        var bank = _context.CurrentBank;
        await _authorizationService.RegisterEmployeeAsync(user.Id, bank.Id, role);
    }
}