using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    public Task AddAsync(Loan loan)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Loan loan)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Loan loan)
    {
        throw new NotImplementedException();
    }

    public Task<Loan> GetByIdAsync(int loanId)
    {
        throw new NotImplementedException();
    }
}