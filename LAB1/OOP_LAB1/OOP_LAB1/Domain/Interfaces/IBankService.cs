using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IBankService
{
    public void CreateBank();

    public IEnumerable<string> GetAllBankNames();
    
    public Bank GetBankByName(string name);
}