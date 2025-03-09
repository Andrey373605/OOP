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

    public async Task<Bank> GetByNameAsync(string name)
    {
        await Task.Delay(1);
        return new Bank { Name = name };
    }

    public Task UpdateAsync(Bank bank)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetAllBankNamesAsync()
    {
        await Task.Delay(1);
        return new List<string>(["ZBANK", "VBANK"]);
    }

    public Task<Employee> GetEmployeeByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Client> GetClientByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}