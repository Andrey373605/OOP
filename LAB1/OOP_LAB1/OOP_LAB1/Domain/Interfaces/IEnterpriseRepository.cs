using OOP_LAB1.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Interfaces
{
    internal interface IEnterpriseRepository
    {
        void Add(Enterprise enterprise);

        Enterprise GetById(int id);

        void Update(Enterprise enterprise);

        void Delete(int id);

        IEnumerable<Enterprise> GetAll();

        Enterprise GetByLegalName(string legalName);

        IEnumerable<Account> GetAccountsByEnterpriseId(int enterpriseId);
    }
}
