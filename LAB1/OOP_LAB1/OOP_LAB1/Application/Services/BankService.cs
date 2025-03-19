using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class BankService : IBankService
{
    private readonly IBankRepository _bankRepository;

    public BankService(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }
    public async Task CreateBank(string bankName)
    {
        var bank = new Bank
        {
            Name = bankName,
        };
        
        await _bankRepository.AddAsync(bank);
    }

    public async Task<List<string>> GetAllBankNames()
    {
        var bankNames = await _bankRepository.GetAllBankNamesAsync();
        return bankNames.ToList();
    }

    public async Task<Bank> GetBankByName(string name)
    {
        var bank = await _bankRepository.GetByNameAsync(name);
        if (bank == null)
        {
            throw new ApplicationException($"Bank {name} not found");
        }
        return bank;
    }

    public async Task<IEnumerable<Bank>> GetAllBanks()
    {
        var bankNames = await _bankRepository.GetAllBanksAsync();
        return bankNames;
    }
}