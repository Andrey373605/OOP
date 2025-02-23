
using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Application.Interfaces;

namespace OOP_LAB1.Application.Services;

internal class AccountService : IAccountService
{

    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void BlockAccount(int accountId)
    {
        Account account = _accountRepository.GetById(accountId);

        if (account == null)
        {
            throw new InvalidOperationException("Account does not exist");
        }

        if (account.IsBlocked)
        {
            throw new InvalidOperationException("Account already bloacked");
        }

        account.IsBlocked = true;
        _accountRepository.Update(account);
    }

    public Account CreateAccount(AccountType type, int UserId)
    {
        Account account = new Account 
        { 
            Balance = 0,
            OwnerId = UserId, 
            IsBlocked = false,
            IsFrozen = false,
        };

        _accountRepository.Add(account);
        return account;
    }

    public void DepositFunds(int accountId, decimal amount)
    {
        Account account = _accountRepository.GetById(accountId);

        if (account == null)
        {
            throw new InvalidOperationException("Account does not exist");
        }

        if (account.IsFrozen)
        {
            throw new InvalidOperationException("Account is frozen");
        }

        if (account.IsBlocked)
        {
            throw new InvalidOperationException("Account is blocked");
        }

        if (amount <= 0)
        {
            throw new InvalidOperationException("Amount must be positive");
        }

        account.Balance += amount;
        _accountRepository.Update(account);
    }

    public Account GetAccountById(int id)
    {
        Account account = _accountRepository.GetById(id);

        if (account == null)
        {
            throw new InvalidOperationException("Account does not exist");
        }

        return account;
    }

    public IEnumerable<Account> GetAccountsByOwnerId(int ownerId)
    {
        List<Account> accounts = _accountRepository.GetByOwnerId(ownerId).ToList();

        if (!accounts.Any())
        {
            throw new InvalidOperationException("Account does not exist");
        }

        return accounts;
    }

    public void TransferFunds(int fromAccountId, int toAccountId, decimal amount)
    {
        var fromAccount = _accountRepository.GetById(fromAccountId);
        var toAccount = _accountRepository.GetById(toAccountId);

        if (fromAccount == null || toAccount == null)
        {
            throw new InvalidOperationException("Один из счетов не найден");
        }

        if (fromAccount.IsBlocked || fromAccount.IsFrozen)
        {
            throw new InvalidOperationException("Счет отправителя заблокирован или заморожен");
        }

        if (fromAccount.Balance < amount)
        {
            throw new InvalidOperationException("Недостаточно средств на счете отправителя");
        }

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        _accountRepository.Update(fromAccount);
        _accountRepository.Update(toAccount);
    }

    public void UnblockAccount(int accountId)
    {
        Account account = _accountRepository.GetById(accountId);

        if (account == null)
        {
            throw new InvalidOperationException("Account does not exist");
        }

        if (!account.IsBlocked)
        {
            throw new InvalidOperationException("Account do not blocked");
        }

        account.IsBlocked = false;
        _accountRepository.Update(account);
    }

    public void WithdrawFunds(int accountId, decimal amount)
    {
        Account account = _accountRepository.GetById(accountId);

        if (account == null)
        {
            throw new InvalidOperationException("Account does not exist");
        }

        if (account.IsFrozen)
        {
            throw new InvalidOperationException("Account is frozen");
        }

        if (account.IsBlocked)
        {
            throw new InvalidOperationException("Account is blocked");
        }

        if (amount <= 0)
        {
            throw new InvalidOperationException("Amount must be positive");
        }

        account.Balance -= amount;
        _accountRepository.Update(account);
    }
}