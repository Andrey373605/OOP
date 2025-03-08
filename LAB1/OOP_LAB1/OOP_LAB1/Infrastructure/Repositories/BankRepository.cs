using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    public Task AddAsync(Bank bank)
    {
        throw new NotImplementedException();
    }

    public Task<Bank> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Bank> GetByNameAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Bank bank)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetAllBankNamesAsync()
    {
        throw new NotImplementedException();
    }
}