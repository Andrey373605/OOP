using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using SQLitePCL;

namespace OOP_LAB1.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IClientRepository _clientRepository;

    public AccountService(IAccountRepository accountRepository, IBankRepository bankRepository, IClientRepository clientRepository)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task FreezeAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.FreezeAccount();
        await _accountRepository.UpdateAsync(account);
        
    }

    public async Task UnfreezeAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.UnfreezeAccount();
        await _accountRepository.UpdateAsync(account);
    }

    public async Task BlockAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.BlockAccount();
        await _accountRepository.UpdateAsync(account);
    }

    public async Task UnblockAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.UnblockAccount();
        await _accountRepository.UpdateAsync(account);
    }
    

    public async Task DeleteAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        await _accountRepository.RemoveAsync(account);
    }

    public async Task DepositAccountAsync(int accountId, decimal amount)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.DepositAccount(amount);
        await _accountRepository.UpdateAsync(account);
    }

    public async Task<IEnumerable<Account>> GetAllClientAccountsAsync(IContext context)
    {
        var user = context.CurrentUser;
        var bank = context.CurrentUser;
        var client = await _bankRepository.GetClientByUserIdAsync(bank.Id, user.Id);
        var accounts = await _clientRepository.GetAllAccountsByClientIdAsync(client.Id);
        return accounts;
    }

    public async Task CreateAccountAsync(int clientId, int bankId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new NullReferenceException("User does not exist");
        }
        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new NullReferenceException("Bank does not exist");
        }
        
        Account account = new Account
        {
            ClientId = client.Id,
            BankId = bank.Id,
            Balance = 0,
            IsFrozen = false,
            IsBlocked = false,
            AccountType = AccountType.Saving
        };
        await _accountRepository.AddAsync(account);
    }
    

    
}