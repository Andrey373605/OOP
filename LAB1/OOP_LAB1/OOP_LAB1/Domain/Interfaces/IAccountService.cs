using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IAccountService
{
    public Task FreezeAccountAsync(int id);
    public Task UnfreezeAccountAsync(int id);
    public Task BlockAccountAsync(int id);
    public Task UnblockAccountAsync(int id);
    public Task CreateAccountAsync(int clientId);
    public Task DeleteAccountAsync(int id);
    public Task DepositAccountAsync(int id, decimal amount);

    Task<IEnumerable<Account>> GetAllClientAccountsAsync(int clientId);
    Task<bool> IsAccountBelongToClient(int accountId, int clientId);
    Task<Account> GetByIdAsync(int accountId);
}