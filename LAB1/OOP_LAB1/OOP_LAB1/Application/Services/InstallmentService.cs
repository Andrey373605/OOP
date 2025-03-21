using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IDataBaseHelper _dataBaseHelper;
    private readonly ILogger _logger;

    public InstallmentService(IInstallmentRepository installmentRepository, IAccountRepository accountRepository,
        IClientRepository clientRepository, IBankRepository bankRepository, IDataBaseHelper dataBaseHelper, ILogger logger)
    {
        _installmentRepository = installmentRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
        _bankRepository = bankRepository;
        _dataBaseHelper = dataBaseHelper;
        _logger = logger;
    }
    
    public async Task DepositMoney(int installmentId)
    {
        try
        {
            _logger.Information($"Attempting to deposit money for installment with ID: {installmentId}");
            
            var installment = await _installmentRepository.GetByIdAsync(installmentId);
            if (installment == null)
            {
                _logger.Error($"Installment with ID {installmentId} not found");
                throw new ApplicationException($"Installment with id: {installmentId} does not exist");
            }

            var account = await _accountRepository.GetByIdAsync(installment.AccountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {installment.AccountId} not found for installment with ID {installmentId}");
                throw new ApplicationException($"Account with id: {installment.AccountId} does not exist");
            }

            var sum = installment.CalculateMonthlyPayment();
            account.WithdrawAccount(sum);
            installment.DecreaseRestMonth();
            
            await _accountRepository.UpdateAsync(account);
            await _installmentRepository.UpdateAsync(installment);
            _logger.Information($"Successfully deposited money for installment with ID {installmentId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error depositing money for installment with ID {installmentId}");
            throw;
        }
    }

    public async Task CreateInstallmentRequest(int clientId, decimal depositAmount, int monthCount)
    {
        try
        {
            _logger.Information($"Attempting to create installment request for client with ID: {clientId}");
            
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                _logger.Error($"Client with ID {clientId} not found");
                throw new ApplicationException("Client not found");
            }

            var account = new Account
            {
                Balance = 0,
                AccountType = AccountType.Installment,
                Status = AccountStatus.Blocked,
                ClientId = client.Id,
                BankId = client.BankId
            };
            
            await _accountRepository.AddAsync(account);
            var accountId = _dataBaseHelper.GetLastInsertId();
            
            Installment installmentRequest = new Installment
            {
                ClientId = client.Id,
                AccountId = accountId,
                NumberOfPayments = monthCount,
                RestMonth = monthCount,
                Amount = depositAmount,
                Status = InstallmentStatus.Application
            };

            await _installmentRepository.AddAsync(installmentRequest);
            _logger.Information($"Successfully created installment request for client with ID {clientId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error creating installment request for client with ID {clientId}");
            throw;
        }
    }

    public async Task ApproveInstallmentRequest(int installmentId)
    {
        try
        {
            _logger.Information($"Attempting to approve installment request with ID: {installmentId}");
            
            var installmentRequest = await _installmentRepository.GetByIdAsync(installmentId);
            if (installmentRequest == null)
            {
                _logger.Error($"Installment request with ID {installmentId} not found");
                throw new ApplicationException($"Installment request with id: {installmentId} does not exist");
            }
            
            installmentRequest.Activate();
            
            var account = await _accountRepository.GetByIdAsync(installmentRequest.AccountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {installmentRequest.AccountId} not found for installment request with ID {installmentId}");
                throw new ApplicationException($"Account with id: {installmentRequest.AccountId} does not exist");
            }

            account.DepositAccount(installmentRequest.Amount);
            account.Status = AccountStatus.Active;

            await _accountRepository.UpdateAsync(account);
            await _installmentRepository.UpdateAsync(installmentRequest);
            _logger.Information($"Successfully approved installment request with ID {installmentId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error approving installment request with ID {installmentId}");
            throw;
        }
    }

    public async Task RejectInstallmentRequest(int installmentId)
    {
        try
        {
            _logger.Information($"Attempting to reject installment request with ID: {installmentId}");
            
            var installmentRequest = await _installmentRepository.GetByIdAsync(installmentId);
            if (installmentRequest == null)
            {
                _logger.Error($"Installment request with ID {installmentId} not found");
                throw new ApplicationException($"Installment request with id: {installmentId} does not exist");
            }
            
            installmentRequest.Reject();
            
            await _installmentRepository.UpdateAsync(installmentRequest);
            _logger.Information($"Successfully rejected installment request with ID {installmentId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error rejecting installment request with ID {installmentId}");
            throw;
        }
    }

    public async Task<IEnumerable<Installment>> GetAllClientInstallments(int clientId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve all installments for client with ID: {clientId}");
            
            var installments = await _installmentRepository.GetAllByClientId(clientId);
            _logger.Information($"Successfully retrieved all installments for client with ID {clientId}");
            return installments;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving all installments for client with ID {clientId}");
            throw;
        }
    }

    public async Task<IEnumerable<Installment>> GetInstallmentApplications()
    {
        try
        {
            _logger.Information("Attempting to retrieve all installment applications");
            
            var applications = await _installmentRepository.GetInstallmentApplications();
            _logger.Information("Successfully retrieved all installment applications");
            return applications;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all installment applications");
            throw;
        }
    }
}