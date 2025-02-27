using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IDepositService
{
    public Task ApproveDepositAsync(int depositId);
    
    public Task DepositMoneyAsync(int id, decimal depositAmount);

    public Task AddAsync(int idUser, decimal depositAmount, decimal interestRate, int monthCount);
}