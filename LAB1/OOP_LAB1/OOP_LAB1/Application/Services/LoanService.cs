using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class LoanService : ILoanService
{
    readonly ILoanRepository _loanRepository;
    readonly IAccountRepository _accountRepository;

    LoanService(ILoanRepository depositRepository, IAccountRepository accountRepository)
    {
        _loanRepository = depositRepository;
        _accountRepository = accountRepository;
    }
    
    public async Task ApproveLoanRequest(int loanId)
    {
        
        var loanRequest = await _loanRepository.GetByIdAsync(loanId);

        var account = new Account
        {
            Balance = 0,
            AccountType = AccountType.Loan,
            IsBlocked = false,
            IsFrozen = false,
            OwnerId = loanRequest.UserId
        };
        
        loanRequest.IsActive = true;
        await _accountRepository.AddAsync(account);
        await _loanRepository.UpdateAsync(loanRequest);
    }
    
    public async Task DepositMoney(int loanId)
    {
        Loan loan = await _loanRepository.GetByIdAsync(loanId);
        
        Account account = await _accountRepository.GetByIdAsync(loan.AccountId);

        var sum = loan.CalculateMonthlyPayment();
        account.WithdrawAccount(sum);
        loan.RestMonth--;
        
        await _accountRepository.UpdateAsync(account);
        await _loanRepository.UpdateAsync(loan);
    }



    public async Task AddLoanRequest(int idUser, decimal depositAmount, int interestRate, int monthCount)
    {
        
        Loan loanRequest = new Loan
        {
            UserId = idUser,
            MonthCount = monthCount,
            InterestRate = interestRate,
            Amount = depositAmount,
            IsActive = false
        };

        await _loanRepository.CreateAsync(loanRequest);
    }
}