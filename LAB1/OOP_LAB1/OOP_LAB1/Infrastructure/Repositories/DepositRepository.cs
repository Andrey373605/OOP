using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class DepositRepository : IDepositRepository
{
    public void Add(int userId, decimal depositAmount, decimal depositInterestRate)
    {
        throw new NotImplementedException();
    }

    public Task CreateRequestAsync(DepositRequest depositRequest)
    {
        throw new NotImplementedException();
    }
}