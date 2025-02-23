using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;


namespace OOP_LAB1.Application.Interfaces
{
    internal interface IAccountRepository
    {
        void Add(Account account);

        Account GetById(int id);

        void Update(Account account);

        void Delete(int id);

        IEnumerable<Account> GetAll();

        IEnumerable<Account> GetByOwnerId(int ownerId);

        IEnumerable<Account> GetByType(AccountType type);
    }
}
