using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IDepositService
{
    public void CreateDepositAccount(int userId, decimal depositAmount, decimal interestRate);
    
    public void DepositMoney(decimal depositAmount);

    public Task AddDepositRequest(int idUser, decimal depositAmount, decimal interestRate, int monthCount);
}