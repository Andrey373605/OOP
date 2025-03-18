using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IDataBaseHelper _dataBaseHelper;

    public LoanService(ILoanRepository depositRepository, IAccountRepository accountRepository,
        IClientRepository clientRepository, IDataBaseHelper dataBaseHelper)
    {
        _loanRepository = depositRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
        _dataBaseHelper = dataBaseHelper;
    }
    

    public async Task ApproveLoanRequest(int loanId)
    {
        var loanRequest = await _loanRepository.GetByIdAsync(loanId);
        if (loanRequest == null)
        {
            throw new ApplicationException($"Loan request with id: {loanId} does not exist");
        }
        
        loanRequest.Activate();
        
        var account = await _accountRepository.GetByIdAsync(loanRequest.AccountId);
        account.DepositAccount(loanRequest.Amount);
        account.Status = AccountStatus.Active;

        await _accountRepository.UpdateAsync(account);
        await _loanRepository.UpdateAsync(loanRequest);
    }

    public async Task<IEnumerable<Loan>> GetAllClientLoansAsync(int clientId)
    {
        return await _loanRepository.GetAllByClientId(clientId);
    }

    public async Task<IEnumerable<Loan>> GetLoanApplicationsAsync()
    {
        return await _loanRepository.GetLoanApplications();
    }

    public async Task RejectLoanRequest(int id)
    {
        var loanRequest = await _loanRepository.GetByIdAsync(id);
        if (loanRequest == null)
        {
            throw new ApplicationException($"Loan request with id: {id} does not exist");
        }
        
        loanRequest.Reject();
        
        await _loanRepository.UpdateAsync(loanRequest);
    }

    public async Task DepositMoney(int loanId)
    {
        Loan loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null)
        {
            throw new ArgumentException("Loan not found");
        }
        
        Account account = await _accountRepository.GetByIdAsync(loan.AccountId);
        if (account == null)
        {
            throw new ArgumentException("Loan doesnt have an account");
        }

        var sum = loan.CalculateMonthlyPayment();
        account.WithdrawAccount(sum);
        loan.RestMonth--;
        
        await _accountRepository.UpdateAsync(account);
        await _loanRepository.UpdateAsync(loan);
    }



    public async Task CreateLoanRequest(int clientId, decimal depositAmount, int interestRate, int monthCount)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
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
        var accointId = _dataBaseHelper.GetLastInsertId();
        

        Loan loanRequest = new Loan
        {
            ClientId = client.Id,
            AccountId = accointId,
            NumberOfPayments = monthCount,
            RestMonth = monthCount,
            InterestRate = interestRate,
            Amount = depositAmount,
            Status = LoanStatus.Application
        };

        await _loanRepository.AddAsync(loanRequest);
    }
}