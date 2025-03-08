using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IAccountEnterpriseRepository
{
    Task<Account> GetByIdAsync(int accountId);
    Task UpdateAsync(Account projectAccount);
}