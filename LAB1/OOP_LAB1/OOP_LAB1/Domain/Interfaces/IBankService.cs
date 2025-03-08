using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IBankService
{
    public Task CreateBank(string bankName);

    public Task<List<string>> GetAllBankNames();
    
    public Task<Bank> GetBankByName(string name);

}