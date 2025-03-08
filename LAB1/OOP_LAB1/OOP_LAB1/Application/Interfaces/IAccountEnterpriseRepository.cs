using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IAccountEnterpriseRepository
{
    Task<EnterpriseAccount> GetByIdAsync(int accountId);
    Task UpdateAsync(EnterpriseAccount projectAccount);
}