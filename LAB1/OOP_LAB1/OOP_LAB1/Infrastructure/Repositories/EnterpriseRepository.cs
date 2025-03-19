using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class EnterpriseRepository : IEnterpriseRepository
{
    public Task<Enterprise> GetByIdAsync(int enterpriseid)
    {
        throw new NotImplementedException();
    }
}