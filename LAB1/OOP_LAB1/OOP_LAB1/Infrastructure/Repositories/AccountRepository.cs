using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task UpdateAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Account>> GetAllByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}