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
    
    public async Task<(bool, string)> WithdrawFunds(decimal amount, int accountId)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            account.WithdrawAccount(amount);

            await _accountRepository.UpdateAsync(account);
            
            var transaction = new Transaction
            {
                FromAccountId = accountId,
                ToAccountId = null,
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = TransactionType.Withdraw
            };
            
            await _transactionRepository.AddAsync(transaction);
            
            return (true, "successful withdraw");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
    }

    public async Task<(bool, string)> TransferFunds(decimal amount, int fromAccountId, int toAccountId)
    {
        try
        {
            var fromAccount = await _accountRepository.GetByIdAsync(fromAccountId);
            var toAccount = await _accountRepository.GetByIdAsync(toAccountId);
            fromAccount.WithdrawAccount(amount);
            toAccount.DepositAccount(amount);
            await _accountRepository.UpdateAsync(fromAccount);
            await _accountRepository.UpdateAsync(toAccount);
            
            var transaction = new Transaction
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = TransactionType.Withdraw
            };
            
            await _transactionRepository.AddAsync(transaction);
            
            return (true, "successful transfer");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
    }

    public async Task<(bool, string)> DepositFunds(decimal amount, int accountId)
    {
        try
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
                Type = TransactionType.Withdraw
            };
            
            await _transactionRepository.AddAsync(transaction);
            
            return (true, "successful deposit");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
    }
}