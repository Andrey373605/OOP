using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class BankService : IBankService
{
    public void CreateBank()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetAllBankNames()
    {
        return new List<string>(["ZBank", "TBank", "VBank"]);
    }

    public Bank GetBankByName(string name)
    {
        return new Bank{ Name = name };
    }
}