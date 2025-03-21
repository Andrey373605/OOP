using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ILogger _logger;

    public AccountService(IAccountRepository accountRepository, IBankRepository bankRepository, IClientRepository clientRepository, ILogger logger)
    {
        _accountRepository = accountRepository;
        _bankRepository = bankRepository;
        _clientRepository = clientRepository;
        _logger = logger;
    }
    
    public async Task FreezeAccountAsync(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to freeze account with ID: {accountId}");
            
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }

            if (account.Status != AccountStatus.Active)
            {
                _logger.Error($"Account with ID {accountId} is not active");
                throw new UnauthorizedAccessException("Account is not active");
            }
            
            account.FreezeAccount();
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Account with ID {accountId} has been frozen");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error freezing account with ID {accountId}");
            throw;
        }
    }

    public async Task UnfreezeAccountAsync(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to unfreeze account with ID: {accountId}");
            
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }
            
            if (account.Status != AccountStatus.Frozen)
            {
                _logger.Error($"Account with ID {accountId} is not frozen");
                throw new UnauthorizedAccessException("Account is not frozen");
            }
            
            account.UnfreezeAccount();
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Account with ID {accountId} has been unfrozen");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error unfreezing account with ID {accountId}");
            throw;
        }
    }

    public async Task BlockAccountAsync(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to block account with ID: {accountId}");
            
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }
            
            account.BlockAccount();
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Account with ID {accountId} has been blocked");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error blocking account with ID {accountId}");
            throw;
        }
    }

    public async Task UnblockAccountAsync(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to unblock account with ID: {accountId}");
            
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }
            
            account.UnblockAccount();
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Account with ID {accountId} has been unblocked");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error unblocking account with ID {accountId}");
            throw;
        }
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to delete account with ID: {accountId}");
            
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }
            
            account.Status = AccountStatus.Deleted;
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Account with ID {accountId} has been deleted");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error deleting account with ID {accountId}");
            throw;
        }
    }

    public async Task DepositAccountAsync(int accountId, decimal amount)
    {
        try
        {
            _logger.Information($"Attempting to deposit {amount} into account with ID: {accountId}");
            
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }
            
            account.DepositAccount(amount);
            await _accountRepository.UpdateAsync(account);
            _logger.Information($"Deposited {amount} into account with ID {accountId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error depositing {amount} into account with ID {accountId}");
            throw;
        }
    }

    public async Task<IEnumerable<Account>> GetAllClientAccountsAsync(int clientId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve all accounts for client with ID: {clientId}");
            
            var accounts = await _clientRepository.GetAllAccountsByClientIdAsync(clientId);
            _logger.Information($"Successfully retrieved all accounts for client with ID {clientId}");
            return accounts.ToList();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving all accounts for client with ID {clientId}");
            throw;
        }
    }

    public async Task<bool> IsAccountBelongToClient(int accountId, int clientId)
    {
        try
        {
            _logger.Information($"Checking if account with ID {accountId} belongs to client with ID {clientId}");
            
            var account = await _accountRepository.GetByIdAsync(accountId);
            bool result = account?.ClientId == clientId;
            _logger.Information($"Account with ID {accountId} {(result ? "belongs" : "does not belong")} to client with ID {clientId}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error checking if account with ID {accountId} belongs to client with ID {clientId}");
            throw;
        }
    }

    public async Task<Account> GetByIdAsync(int accountId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve account with ID: {accountId}");
            
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
            {
                _logger.Error($"Account with ID {accountId} does not exist");
                throw new NullReferenceException("Account not found");
            }
            _logger.Information($"Successfully retrieved account with ID {accountId}");
            return account;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving account with ID {accountId}");
            throw;
        }
    }

    public async Task CreateAccountAsync(int clientId)
    {
        try
        {
            _logger.Information($"Attempting to create account for client with ID: {clientId}");
            
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                _logger.Error($"Client with ID {clientId} does not exist");
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
            _logger.Information($"Account created successfully for client with ID {clientId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error creating account for client with ID {clientId}");
            throw;
        }
    }
}