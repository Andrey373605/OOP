using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class LoanService : ILoanService
{
    readonly ILoanRepository _loanRepository;

    LoanService(ILoanRepository depositRepository)
    {
        _loanRepository = depositRepository;
    }
    
    

    public Task AddLoanRequest()
    {
        throw new NotImplementedException();
    }

    public void CreateLoanAccount(int userId, decimal depositAmount, decimal interestRate)
    {
        throw new NotImplementedException();
    }

    public void DepositMoney(decimal depositAmount)
    {
        throw new NotImplementedException();
    }

    public Task AddLoanRequest(int idUser, decimal depositAmount, decimal interestRate, int monthCount)
    {
        LoanRequest loanRequest = new LoanRequest
        {
            UserId = idUser,
            MonthCount = monthCount,
            InterestRate = interestRate,
            Amount = depositAmount
        };

        return _loanRepository.CreateRequestAsync(loanRequest);
    }
}