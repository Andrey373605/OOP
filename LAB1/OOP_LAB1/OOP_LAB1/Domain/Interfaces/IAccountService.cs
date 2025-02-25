using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IAccountService
{
    void FreezeAccount(int id);
    void UnfreezeAccount(int id);
    void BlockAccount(int id);
    void UnblockAccount(int id);
    void CreateAccount();
    void DeleteAccount();

    void DepositAccount();
}