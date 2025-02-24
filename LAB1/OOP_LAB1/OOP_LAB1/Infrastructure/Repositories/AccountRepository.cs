using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    public void Add(Account account)
    {
        throw new NotImplementedException();
    }

    public Account GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Account account)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Account> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Account> GetByOwnerId(int ownerId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Account> GetByType(AccountType type)
    {
        throw new NotImplementedException();
    }
}