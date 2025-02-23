using OOP_LAB1.Domain.Entities;


namespace OOP_LAB1.Domain.Interfaces
{
    public interface IBankService
    {
        Bank CreateBank(string name);

        Bank GetBankById(int id);

        IEnumerable<Bank> GetAllBanks();

        void AddEnterpriseToBank(int bankId, int enterpriseId);

        IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId);
        
        void DeleteBank(int id);
    }
}
