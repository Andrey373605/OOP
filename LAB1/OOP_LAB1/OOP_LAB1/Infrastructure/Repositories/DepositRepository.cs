using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class DepositRepository : IDepositRepository
{
    public Task AddAsync(Deposit deposit)
    {
        throw new NotImplementedException();
    }

    public Task<Deposit> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Deposit deposit)
    {
        throw new NotImplementedException();
    }
}