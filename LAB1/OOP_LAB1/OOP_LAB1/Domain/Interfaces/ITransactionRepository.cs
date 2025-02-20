using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Enteties;

namespace OOP_LAB1.Domain.Interfaces
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
