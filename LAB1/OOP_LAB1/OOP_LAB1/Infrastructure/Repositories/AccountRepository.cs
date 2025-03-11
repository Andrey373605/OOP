using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;

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

    public async Task<IEnumerable<Account>> GetAllByUserIdAsync(int userId)
    {
        await Task.Delay(1);
        return new List<Account>([new Account { Id = userId, Balance = 100, AccountType = AccountType.Saving}, 
                                            new Account { Id = userId, Balance = 200, AccountType = AccountType.Loan}]);
    }
}