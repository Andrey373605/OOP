using OOP_LAB1.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Interfaces
{
    internal interface IBankRepository
    {
        void Add(Bank bank);

        Bank GetById(int id);

        void Update(Bank bank);

        // Удаление банка
        void Delete(int id);

        IEnumerable<Bank> GetAll();

        Bank GetByName(string name);

        IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId);
    }
}
