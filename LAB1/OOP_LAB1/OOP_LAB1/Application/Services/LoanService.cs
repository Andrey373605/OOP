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

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IDataBaseHelper _dataBaseHelper;
    private readonly ILogger _logger;

    public LoanService(ILoanRepository loanRepository, IAccountRepository accountRepository,
        IClientRepository clientRepository, IDataBaseHelper dataBaseHelper, ILogger logger)
    {
        _loanRepository = loanRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
        _dataBaseHelper = dataBaseHelper;
        _logger = logger;
    }
    
    public async Task ApproveLoanRequest(int loanId)
    {
        try
        {
            _logger.Information($"Attempting to approve loan request with ID: {loanId}");
            
            var loanRequest = await _loanRepository.GetByIdAsync(loanId);
            if (loanRequest == null)
            {
                _logger.Error($"Loan request with ID {loanId} not found");
                throw new ApplicationException($"Loan request with id: {loanId} does not exist");
            }
            
            loanRequest.Activate();
            
            var account = await _accountRepository.GetByIdAsync(loanRequest.AccountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {loanRequest.AccountId} not found for loan request with ID {loanId}");
                throw new ApplicationException($"Account with id: {loanRequest.AccountId} does not exist");
            }

            account.DepositAccount(loanRequest.Amount);
            account.Status = AccountStatus.Active;

            await _accountRepository.UpdateAsync(account);
            await _loanRepository.UpdateAsync(loanRequest);
            _logger.Information($"Successfully approved loan request with ID {loanId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error approving loan request with ID {loanId}");
            throw;
        }
    }

    public async Task<IEnumerable<Loan>> GetAllClientLoans(int clientId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve all loans for client with ID: {clientId}");
            
            var loans = await _loanRepository.GetAllByClientId(clientId);
            _logger.Information($"Successfully retrieved all loans for client with ID {clientId}");
            return loans;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving all loans for client with ID {clientId}");
            throw;
        }
    }

    public async Task<IEnumerable<Loan>> GetLoanApplications()
    {
        try
        {
            _logger.Information("Attempting to retrieve all loan applications");
            
            var applications = await _loanRepository.GetLoanApplications();
            _logger.Information("Successfully retrieved all loan applications");
            return applications;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all loan applications");
            throw;
        }
    }

    public async Task RejectLoanRequest(int id)
    {
        try
        {
            _logger.Information($"Attempting to reject loan request with ID: {id}");
            
            var loanRequest = await _loanRepository.GetByIdAsync(id);
            if (loanRequest == null)
            {
                _logger.Error($"Loan request with ID {id} not found");
                throw new ApplicationException($"Loan request with id: {id} does not exist");
            }
            
            loanRequest.Reject();
            
            await _loanRepository.UpdateAsync(loanRequest);
            _logger.Information($"Successfully rejected loan request with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error rejecting loan request with ID {id}");
            throw;
        }
    }

    public async Task DepositMoney(int loanId)
    {
        try
        {
            _logger.Information($"Attempting to deposit money for loan with ID: {loanId}");
            
            Loan loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
            {
                _logger.Error($"Loan with ID {loanId} not found");
                throw new ArgumentException("Loan not found");
            }
            
            Account account = await _accountRepository.GetByIdAsync(loan.AccountId);
            if (account == null)
            {
                _logger.Error($"Account with ID {loan.AccountId} not found for loan with ID {loanId}");
                throw new ArgumentException("Loan does not have an account");
            }

            var sum = loan.CalculateMonthlyPayment();
            account.WithdrawAccount(sum);
            loan.RestMonth--;
            
            await _accountRepository.UpdateAsync(account);
            await _loanRepository.UpdateAsync(loan);
            _logger.Information($"Successfully deposited money for loan with ID {loanId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error depositing money for loan with ID {loanId}");
            throw;
        }
    }

    public async Task CreateLoanRequest(int clientId, decimal depositAmount, int interestRate, int monthCount)
    {
        try
        {
            _logger.Information($"Attempting to create loan request for client with ID: {clientId}");
            
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                _logger.Error($"Client with ID {clientId} not found");
                throw new ApplicationException("Client not found");
            }

            var account = new Account
            {
                Balance = 0,
                AccountType = AccountType.Loan,
                Status = AccountStatus.Blocked,
                ClientId = client.Id,
                BankId = client.BankId
            };
            
            await _accountRepository.AddAsync(account);
            var accountId = _dataBaseHelper.GetLastInsertId();
            
            Loan loanRequest = new Loan
            {
                ClientId = client.Id,
                AccountId = accountId,
                NumberOfPayments = monthCount,
                RestMonth = monthCount,
                InterestRate = interestRate,
                Amount = depositAmount,
                Status = LoanStatus.Application
            };

            await _loanRepository.AddAsync(loanRequest);
            _logger.Information($"Successfully created loan request for client with ID {clientId}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error creating loan request for client with ID {clientId}");
            throw;
        }
    }
}