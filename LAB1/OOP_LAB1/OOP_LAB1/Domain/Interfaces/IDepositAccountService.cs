using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IDepositAccountService
{
    public void CreateDepositAccount(DepositAccount depositAccount);
    
    public void DepositMoney(decimal depositAmount);
}