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
    
    public void FreezeAccount(int id)
    {
        Account account = _accountRepository.GetById(id);
        account.FreezeAccount();
        _accountRepository.Update(account);
    }

    public void UnfreezeAccount(int id)
    {
        Account account = _accountRepository.GetById(id);
        account.UnfreezeAccount();
        _accountRepository.Update(account);
    }

    public void BlockAccount(int id)
    {
        Account account = _accountRepository.GetById(id);
        account.BlockAccount();
        _accountRepository.Update(account);
    }

    public void UnblockAccount(int id)
    {
        Account account = _accountRepository.GetById(id);
        account.UnblockAccount();
        _accountRepository.Update(account);
    }

    public void CreateAccount(int userId)
    {
        _accountRepository.Add(userId);
    }

    public void DeleteAccount(int id)
    {
        Account account = _accountRepository.GetById(id);

        if (account == null)
        {
            throw new NullReferenceException();
        }
        
        _accountRepository.Remove(account);
    }

    public void DepositAccount(int id, decimal amount)
    {
        Account account = _accountRepository.GetById(id);
        account.DepositAccount(amount);
        _accountRepository.Update(account);
    }


}