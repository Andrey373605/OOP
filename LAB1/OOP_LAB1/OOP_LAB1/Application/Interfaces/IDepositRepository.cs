using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface IDepositRepository
{
    
    public Task AddAsync(Deposit deposit);
    
    public Task<Deposit> GetByIdAsync(int id);
    
    public Task UpdateAsync(Deposit deposit);
    
}