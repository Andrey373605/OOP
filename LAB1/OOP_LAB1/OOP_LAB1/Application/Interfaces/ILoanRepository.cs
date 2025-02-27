using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface ILoanRepository
{
    public Task AddAsync(Loan loan);
    
    public Task CreateAsync(Loan loan);
    
    public Task UpdateAsync(Loan loan);
    
    public Task DeleteAsync(Loan loan);
}