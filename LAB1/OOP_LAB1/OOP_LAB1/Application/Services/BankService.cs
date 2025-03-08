using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class BankService : IBankService
{
    IContext _context;
    IBankRepository _bankRepository;

    public BankService(IContext context, IBankRepository bankRepository)
    {
        _context = context;
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
        return bank;
    }

    
}