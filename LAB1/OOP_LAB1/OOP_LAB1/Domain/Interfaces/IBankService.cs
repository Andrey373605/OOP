using OOP_LAB1.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Services
{
    internal interface IBankService
    {
        Bank CreateBank(string name);

        Bank GetBankById(int id);

        IEnumerable<Bank> GetAllBanks();

        void AddEnterpriseToBank(int bankId, Enterprise enterprise);

        IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId);

        void UpdateBank(Bank bank);

        void DeleteBank(int id);
    }
}
