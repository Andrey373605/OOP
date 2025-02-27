using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface ILoanRepository
{
    public void Add(int userId, decimal depositAmount, decimal depositInterestRate);
    
    public Task CreateRequestAsync(LoanRequest loanRequest);
}