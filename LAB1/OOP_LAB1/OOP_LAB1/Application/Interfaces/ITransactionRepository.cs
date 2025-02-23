using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories
{
    internal interface ITransactionRepository
    {
        void Add(Transaction transaction);

        Transaction GetById(int id);

        void Update(Transaction transaction);

        void Delete(int id);

        IEnumerable<Transaction> GetAll();

        IEnumerable<Transaction> GetByAccountId(int accountId);

        IEnumerable<Transaction> GetByType(TransactionType type);
    }
}
