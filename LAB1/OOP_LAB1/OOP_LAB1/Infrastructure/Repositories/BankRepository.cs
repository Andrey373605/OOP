using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    public void Add(Bank bank)
    {
        throw new NotImplementedException();
    }

    public Bank GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Bank bank)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bank> GetAll()
    {
        throw new NotImplementedException();
    }

    public Bank GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId)
    {
        throw new NotImplementedException();
    }
}