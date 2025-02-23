using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Infrastructure.Repositories;


namespace OOP_LAB1.Application.Services
{
    internal class BankService : IBankService
    {
        readonly IBankRepository _bankRepository;
        readonly IEnterpriseRepository _enterpriseRepository;
        public BankService(IBankRepository bankRepository, IEnterpriseRepository enterpriseRepository) 
        { 
            _bankRepository = bankRepository;
            _enterpriseRepository = enterpriseRepository;
        }
        public void AddEnterpriseToBank(int bankId, int enterpriseId)
        {
            var bank = _bankRepository.GetById(bankId);
            if (bank == null)
            {
                throw new InvalidOperationException("Bank not found");
            }
            
            var enterprise = _enterpriseRepository.GetById(enterpriseId);
            if (enterprise == null)
            {
                throw new InvalidOperationException("Enterprise not found");
            }
            
            if (bank.EnterprisesIdList.Contains(enterpriseId))
            {
                throw new InvalidOperationException("Enterprise is already associated with this bank");
            }
            
            bank.EnterprisesIdList.Add(enterpriseId);
            
            _bankRepository.Update(bank);

        }

        public Bank CreateBank(string name)
        {
            Bank bank = _bankRepository.GetByName(name);
            if (bank != null)
            {
                throw new InvalidOperationException("Bank already exists");
            }
            Bank newBank = new Bank
            {
                Name = name,
                UsersIdList = new List<int>(),
                EnterprisesIdList = new List<int>()
            };
            return newBank;
        }

        public void DeleteBank(int id)
        {
            Bank bank = _bankRepository.GetById(id);
            if (bank == null)
            {
                throw new InvalidOperationException("Bank does not exist");
            }
            
            _bankRepository.Delete(id);
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            return _bankRepository.GetAll();
        }

        public Bank GetBankById(int id)
        {
            Bank bank = _bankRepository.GetById(id);
            if (bank == null)
            {
                throw new InvalidOperationException("Bank does not exist");
            }

            return bank;
        }

        public IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId)
        {
            return _bankRepository.GetEnterprisesByBankId(bankId);
        }
        
    }
}
