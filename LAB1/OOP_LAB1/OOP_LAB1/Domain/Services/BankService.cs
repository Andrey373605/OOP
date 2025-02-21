using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Services
{
    internal class BankService : IBankService
    {
        IBankRepository _bankRepository;
        public BankService(IBankRepository bankRepository) 
        { 
            _bankRepository = bankRepository;
        }
        public void AddEnterpriseToBank(int bankId, Enterprise enterprise)
        {
            Bank bank = _bankRepository.GetById(bankId);

            if (bank == null)
            {
                throw new InvalidOperationException("Bank does not exist");
            }

        }

        public Bank CreateBank(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteBank(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            throw new NotImplementedException();
        }

        public Bank GetBankById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBank(Bank bank)
        {
            throw new NotImplementedException();
        }
    }
}
