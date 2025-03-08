using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class LoanService : ILoanService
{
    readonly ILoanRepository _loanRepository;
    readonly IAccountRepository _accountRepository;
    readonly IClientRepository _clientRepository;

    LoanService(ILoanRepository depositRepository, IAccountRepository accountRepository, IClientRepository clientRepository)
    {
        _loanRepository = depositRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
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
            ClientId = loanRequest.ClientId
        };
        
        loanRequest.SetActive();
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



    public async Task AddLoanRequest(int clientId, decimal depositAmount, int interestRate, int monthCount)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
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