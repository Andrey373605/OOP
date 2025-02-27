using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface IDepositRepository
{
    public void Add(int userId, decimal depositAmount, decimal depositInterestRate);
    
    public Task CreateRequestAsync(DepositRequest depositRequest);
}