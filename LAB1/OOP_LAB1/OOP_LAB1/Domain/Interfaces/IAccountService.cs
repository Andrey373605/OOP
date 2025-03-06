using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IAccountService
{
    Task FreezeAccountAsync(int id);
    Task UnfreezeAccountAsync(int id);
    Task BlockAccountAsync(int id);
    Task UnblockAccountAsync(int id);
    Task CreateAccountAsync(int userId);
    Task DeleteAccountAsync(int id);
    Task DepositAccountAsync(int id, decimal amount);
    
}