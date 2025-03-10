using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public Task<Transaction> GetByIdAsync(int transactionId)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}