using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IBankRepository _bankRepository;

    public LoanService(ILoanRepository depositRepository, IAccountRepository accountRepository,
        IClientRepository clientRepository, IBankRepository bankRepository)
    {
        _loanRepository = depositRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
    }
    

    public async Task ApproveLoanRequest(int loanId)
    {
        var loanRequest = await _loanRepository.GetByIdAsync(loanId);
        if (loanRequest == null)
        {
            throw new ApplicationException($"Loan request with id: {loanId} does not exist");
        }

        var account = new Account
        {
            Balance = 0,
            AccountType = AccountType.Loan,
            Status = AccountStatus.Normal,
            ClientId = loanRequest.ClientId
        };
        
        loanRequest.SetActive();
        await _accountRepository.AddAsync(account);
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
        var client = await _bankRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new ApplicationException("Client not found");
        }

        Loan loanRequest = new Loan
        {
            ClientId = client.Id,
            NumberOfPayments = monthCount,
            InterestRate = interestRate,
            Amount = depositAmount,
            IsActive = false
        };

        await _loanRepository.AddAsync(loanRequest);
    }
}