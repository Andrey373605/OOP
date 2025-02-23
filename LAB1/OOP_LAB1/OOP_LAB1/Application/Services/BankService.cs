using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Application.Interfaces;


namespace OOP_LAB1.Application.Services
{
    internal class BankService : IBankService
    {
        readonly IBankRepository _bankRepository;
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
            if (_bankRepository.GetByName(name) != null)
            {
                throw new InvalidOperationException("Bank already exists");
            }
            Bank bank = new Bank
            {
                Name = name,
            };
            return bank;
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
