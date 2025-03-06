using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class TransactionService : ITransactionService
{
    readonly IAccountRepository _accountRepository;

    public TransactionService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task WithdrawFunds(decimal amount, int accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        account.WithdrawAccount(amount);
        //вывод средств????
        await _accountRepository.UpdateAsync(account);
    }

    public async Task TransferFunds(decimal amount, int fromAccountId, int toAccountId)
    {
        var fromAccount = await _accountRepository.GetByIdAsync(fromAccountId);
        var toAccount = await _accountRepository.GetByIdAsync(toAccountId);
        fromAccount.WithdrawAccount(amount);
        toAccount.DepositAccount(amount);
        await _accountRepository.UpdateAsync(fromAccount);
        await _accountRepository.UpdateAsync(toAccount);
    }

    public async Task DepositFunds(decimal amount, int accountId)
    {
        //обращение к терминалу???
        var account = await _accountRepository.GetByIdAsync(accountId);
        account.DepositAccount(amount);
        await _accountRepository.UpdateAsync(account);
    }
}