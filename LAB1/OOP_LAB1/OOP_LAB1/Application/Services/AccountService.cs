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
    
    public async Task FreezeAccountAsync(int accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }

        if (account.Status != AccountStatus.Active)
        {
            throw new UnauthorizedAccessException("Account is not active");
        }
        
        account.FreezeAccount();
        await _accountRepository.UpdateAsync(account);
        
    }

    public async Task UnfreezeAccountAsync(int accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        if (account.Status != AccountStatus.Frozen)
        {
            throw new UnauthorizedAccessException("Account is not frozen");
        }
        
        account.UnfreezeAccount();
        await _accountRepository.UpdateAsync(account);
    }

    public async Task BlockAccountAsync(int accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.BlockAccount();
        await _accountRepository.UpdateAsync(account);
    }

    public async Task UnblockAccountAsync(int accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.UnblockAccount();
        await _accountRepository.UpdateAsync(account);
    }
    


    public async Task DeleteAccountAsync(int accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        
        account.Status = AccountStatus.Deleted;
        await _accountRepository.UpdateAsync(account);
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

    public async Task<IEnumerable<Account>> GetAllClientAccountsAsync(int clientId)
    {
        var accounts = await _clientRepository.GetAllAccountsByClientIdAsync(clientId);
        return accounts.ToList();
    }

    public async Task<bool> IsAccountBelongToClient(int accountId, int clientId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        return account.ClientId == clientId;
    }

    public async Task<Account> GetByIdAsync(int accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }
        return account;
    }

    public async Task CreateAccountAsync(int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new NullReferenceException("Context user error");
        }
        
        
        Account account = new Account
        {
            ClientId = client.Id,
            BankId = client.BankId,
            Balance = 0,
            Status = AccountStatus.Active,
            AccountType = AccountType.Saving
        };
        await _accountRepository.AddAsync(account);
    }
    

    
}