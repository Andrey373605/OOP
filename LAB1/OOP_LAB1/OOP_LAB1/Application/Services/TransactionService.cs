using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILogger _logger;

    public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ILogger logger)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _logger = logger;
    }
    
    public async Task<bool> WithdrawFunds(decimal amount, int accountId)
    {
        try
        {
            _logger.Information($"Attempting to withdraw {amount} from account with ID: {accountId}");
            
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} not found");
                throw new NullReferenceException("Account does not exist");
            }

            if (amount < 0)
            {
                _logger.Error($"Attempted to withdraw negative amount: {amount} from account with ID {accountId}");
                throw new ApplicationException("Insufficient funds");
            }

            if (amount > account.Balance)
            {
                _logger.Error($"Attempted to withdraw {amount} from account with ID {accountId}, but balance is {account.Balance}");
                throw new ApplicationException("Insufficient funds");
            }
            
            var transaction = new Transaction
            {
                FromAccountId = accountId,
                ToAccountId = null,
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = TransactionType.Withdraw,
                Status = TransactionStatus.Completed
            };
            
            await _transactionRepository.AddAsync(transaction);
            
            account.WithdrawAccount(amount);
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Successfully withdrew {amount} from account with ID {accountId}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error withdrawing {amount} from account with ID {accountId}");
            throw;
        }
    }

    public async Task<bool> TransferFunds(decimal amount, int fromAccountId, int toAccountId)
    {
        try
        {
            _logger.Information($"Attempting to transfer {amount} from account with ID: {fromAccountId} to account with ID: {toAccountId}");
            
            var fromAccount = await _accountRepository.GetByIdAsync(fromAccountId);
            var toAccount = await _accountRepository.GetByIdAsync(toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                _logger.Error($"One or both accounts (from ID {fromAccountId}, to ID {toAccountId}) do not exist");
                throw new NullReferenceException("Account does not exist");
            }

            if (fromAccount.Status != AccountStatus.Active)
            {
                _logger.Error($"From account with ID {fromAccountId} is not active");
                throw new ArgumentException("From Account is not active");
            }

            if (toAccount.Status != AccountStatus.Active)
            {
                _logger.Error($"To account with ID {toAccountId} is not active");
                throw new ArgumentException("To Account is not active");
            }

            if (toAccountId == fromAccountId)
            {
                _logger.Error($"From account ID {fromAccountId} is equal to To account ID {toAccountId}");
                throw new ArgumentException("From Account Id is equal to To Account Id");
            }

            if (fromAccount.Balance < amount)
            {
                _logger.Error($"Insufficient balance in from account with ID {fromAccountId} for transfer of {amount}");
                throw new ArgumentException("Not enough balance");
            }

            if (amount == 0)
            {
                _logger.Error($"Attempted to transfer zero amount from account with ID {fromAccountId} to account with ID {toAccountId}");
                throw new ArgumentException("Amount is equal to 0");
            }
            
            var transaction = new Transaction
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = TransactionType.Transfer,
                Status = TransactionStatus.Completed
            };
            
            await _transactionRepository.AddAsync(transaction);
            
            fromAccount.WithdrawAccount(amount);
            toAccount.DepositAccount(amount);
            await _accountRepository.UpdateAsync(fromAccount);
            await _accountRepository.UpdateAsync(toAccount);
            
            _logger.Information($"Successfully transferred {amount} from account with ID {fromAccountId} to account with ID {toAccountId}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error transferring {amount} from account with ID {fromAccountId} to account with ID {toAccountId}");
            throw;
        }
    }

    public async Task<bool> DepositFunds(decimal amount, int accountId)
    {
        try
        {
            _logger.Information($"Attempting to deposit {amount} into account with ID: {accountId}");
            
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} not found");
                throw new NullReferenceException("Account does not exist");
            }
            
            account.DepositAccount(amount);
            await _accountRepository.UpdateAsync(account);
            
            var transaction = new Transaction
            {
                FromAccountId = null,
                ToAccountId = accountId,
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = TransactionType.Deposit,
                Status = TransactionStatus.Completed
            };
            
            await _transactionRepository.AddAsync(transaction);

            _logger.Information($"Successfully deposited {amount} into account with ID {accountId}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error depositing {amount} into account with ID {accountId}");
            throw;
        }
    }

    public async Task<bool> CancelTransfer(int transferId)
    {
        try
        {
            _logger.Information($"Attempting to cancel transfer with ID: {transferId}");
            
            var transfer = await _transactionRepository.GetByIdAsync(transferId);
            if (transfer == null)
            {
                _logger.Error($"Transfer with ID {transferId} not found");
                throw new NullReferenceException("Transfer does not exist");
            }

            var fromAccount = await _accountRepository.GetByIdAsync(transfer.FromAccountId ?? 0);
            var toAccount = await _accountRepository.GetByIdAsync(transfer.ToAccountId ?? 0);

            if (fromAccount == null || toAccount == null)
            {
                _logger.Error($"One or both accounts (from ID {transfer.FromAccountId}, to ID {transfer.ToAccountId}) do not exist for transfer with ID {transferId}");
                throw new NullReferenceException("This operation cannot be undone");
            }

            if (fromAccount.Status != AccountStatus.Active)
            {
                _logger.Error($"From account with ID {transfer.FromAccountId} is not active for transfer with ID {transferId}");
                throw new ArgumentException("From Account is not active");
            }

            if (toAccount.Status != AccountStatus.Active)
            {
                _logger.Error($"To account with ID {transfer.ToAccountId} is not active for transfer with ID {transferId}");
                throw new ArgumentException("To Account is not active");
            }

            if (toAccount.Id == fromAccount.Id)
            {
                _logger.Error($"From account ID {transfer.FromAccountId} is equal to To account ID {transfer.ToAccountId} for transfer with ID {transferId}");
                throw new ArgumentException("From Account Id is equal to To Account Id");
            }

            if (toAccount.Balance < transfer.Amount)
            {
                _logger.Error($"Insufficient balance in to account with ID {transfer.ToAccountId} for canceling transfer with ID {transferId}");
                throw new ArgumentException("Not enough balance");
            }

            if (transfer.Amount == 0)
            {
                _logger.Error($"Attempted to cancel transfer with ID {transferId} of zero amount");
                throw new ArgumentException("Amount is equal to 0");
            }
            
            var transaction = new Transaction
            {
                FromAccountId = toAccount.Id,
                ToAccountId = fromAccount.Id,
                Amount = transfer.Amount,
                Date = DateTime.UtcNow,
                Type = TransactionType.Transfer,
                Status = TransactionStatus.Canceled
            };
            
            await _transactionRepository.AddAsync(transaction);
            
            toAccount.WithdrawAccount(transfer.Amount);
            fromAccount.DepositAccount(transfer.Amount);
            await _accountRepository.UpdateAsync(fromAccount);
            await _accountRepository.UpdateAsync(toAccount);
            transfer.Delete();
            await _transactionRepository.UpdateAsync(transfer);
            
            _logger.Information($"Successfully canceled transfer with ID {transferId}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error canceling transfer with ID {transferId}");
            throw;
        }
    }

    public async Task<Transaction> GetTransferById(int transferId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve transfer with ID: {transferId}");
            
            var transfer = await _transactionRepository.GetByIdAsync(transferId);
            if (transfer == null)
            {
                _logger.Error($"Transfer with ID {transferId} not found");
                throw new NullReferenceException("Transfer does not exist");
            }
            _logger.Information($"Successfully retrieved transfer with ID {transferId}");
            return transfer;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving transfer with ID {transferId}");
            throw;
        }
    }

    public async Task<IEnumerable<Transaction>> GetTransferByAccountId(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve transfers for account with ID: {accountId}");
            
            var transfers = await _transactionRepository.GetTransferByAccountIdAsync(accountId);
            _logger.Information($"Successfully retrieved transfers for account with ID {accountId}");
            return transfers;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving transfers for account with ID {accountId}");
            throw;
        }
    }

    public async Task<IEnumerable<Transaction>> GetDepositByAccountId(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve deposits for account with ID: {accountId}");
            
            var deposits = await _transactionRepository.GetDepositByAccountIdAsync(accountId);
            _logger.Information($"Successfully retrieved deposits for account with ID {accountId}");
            return deposits;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving deposits for account with ID {accountId}");
            throw;
        }
    }

    public async Task<IEnumerable<Transaction>> GetWithdrawByAccountId(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve withdrawals for account with ID: {accountId}");
            
            var withdrawals = await _transactionRepository.GetWithdrawByAccountIdAsync(accountId);
            _logger.Information($"Successfully retrieved withdrawals for account with ID {accountId}");
            return withdrawals;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving withdrawals for account with ID {accountId}");
            throw;
        }
    }
}