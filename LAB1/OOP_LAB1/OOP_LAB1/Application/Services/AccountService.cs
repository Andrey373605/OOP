using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Services;

public class AccountService : IAccountService
{
    IAccountRepository _accountRepository;

    AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task FreezeAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        account.FreezeAccount();
        await _accountRepository.UpdateAsync(account);
        
    }

    public async Task UnfreezeAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        account.UnfreezeAccount();
        await _accountRepository.UpdateAsync(account);
    }

    public async Task BlockAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        account.BlockAccount();
        await _accountRepository.UpdateAsync(account);
    }

    public async Task UnblockAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        account.UnblockAccount();
        await _accountRepository.UpdateAsync(account);
    }
    

    public async Task DeleteAccountAsync(int id)
    {
        Account account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NullReferenceException();
        }
        
        await _accountRepository.RemoveAsync(account);
    }

    public async Task DepositAccountAsync(int id, decimal amount)
    {
        Account account = await _accountRepository.GetByIdAsync(id);
        account.DepositAccount(amount);
        await _accountRepository.UpdateAsync(account);
    }

    public async Task CreateAccountAsync(int userId)
    {
        Account account = new Account
        {
            OwnerId = userId,
        };
        await _accountRepository.AddAsync(account);
    }
}