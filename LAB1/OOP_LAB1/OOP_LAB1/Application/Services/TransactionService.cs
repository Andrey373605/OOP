using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }
    
    public async Task<bool> WithdrawFunds(decimal amount, int accountId)
    {
        
        var account = await _accountRepository.GetByIdAsync(accountId);

        
        var transaction = new Transaction
        {
            FromAccountId = accountId,
            ToAccountId = null,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = TransactionType.Withdraw
        };
        

        if (transaction.Amount < 0)
        {
            throw new ApplicationException("Insufficient funds");
        }

        if (amount > account.Balance)
        {
            throw new ApplicationException("Insufficient funds");
        }
        
        await _transactionRepository.AddAsync(transaction);
        
        
        account.WithdrawAccount(amount);
        await _accountRepository.UpdateAsync(account);
        return true;
    }

    public async Task<bool> TransferFunds(decimal amount, int fromAccountId, int toAccountId)
    {
        
        var fromAccount = await _accountRepository.GetByIdAsync(fromAccountId);
        var toAccount = await _accountRepository.GetByIdAsync(toAccountId);

        if (fromAccount == null || toAccount == null)
        {
            throw new NullReferenceException("Account dose not exist");
        }

        if (fromAccount.Status != AccountStatus.Active)
        {
            throw new ArgumentException("From Account is not active");
        }

        if (toAccount.Status != AccountStatus.Active)
        {
            throw new ArgumentException("To Account is not active");
        }

        if (toAccountId == fromAccountId)
        {
            throw new ArgumentException("From Account Id is equal to To Account Id");
        }

        if (fromAccount.Balance < amount)
        {
            throw new ArgumentException("Not enough balance");
        }

        if (amount == 0)
        {
            throw new ArgumentException("Amount is equal to 0");
        }
        
        var transaction = new Transaction
        {
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = TransactionType.Transfer
        };
        
        await _transactionRepository.AddAsync(transaction);
        
        fromAccount.WithdrawAccount(amount);
        toAccount.DepositAccount(amount);
        await _accountRepository.UpdateAsync(fromAccount);
        await _accountRepository.UpdateAsync(toAccount);
        
        return true;
    }

    public async Task<bool> DepositFunds(decimal amount, int accountId)
    {
        //обращение к терминалу???
        var account = await _accountRepository.GetByIdAsync(accountId);
        account.DepositAccount(amount);
        await _accountRepository.UpdateAsync(account);
        
        var transaction = new Transaction
        {
            FromAccountId = null,
            ToAccountId = accountId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = TransactionType.Deposit
        };
        
        await _transactionRepository.AddAsync(transaction);

        return true;

    }

    public async Task<Transaction> GetTransferById(int transferId)
    {
        return await _transactionRepository.GetByIdAsync(transferId);
    }

    public async Task<IEnumerable<Transaction>> GetTransferByAccountId(int accountId)
    {
        return await _transactionRepository.GetTransferByAccountIdAsync(accountId);
    }

    public async Task<IEnumerable<Transaction>> GetDepositByAccountId(int accountId)
    {
        return await _transactionRepository.GetDepositByAccountIdAsync(accountId);
    }

    public async Task<IEnumerable<Transaction>> GetWithdrawByAccountId(int accountId)
    {
        return await _transactionRepository.GetWithdrawByAccountIdAsync(accountId);
    }
}