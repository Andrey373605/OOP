using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Interfaces
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
