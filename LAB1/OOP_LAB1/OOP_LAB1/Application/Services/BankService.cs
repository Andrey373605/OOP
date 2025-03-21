using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class BankService : IBankService
{
    private readonly IBankRepository _bankRepository;
    private readonly ILogger _logger;

    public BankService(IBankRepository bankRepository, ILogger logger)
    {
        _bankRepository = bankRepository;
        _logger = logger;
    }
    
    public async Task CreateBank(string bankName)
    {
        try
        {
            _logger.Information($"Attempting to create bank with name: {bankName}");
            
            var bank = new Bank
            {
                Name = bankName,
            };
            
            await _bankRepository.AddAsync(bank);
            _logger.Information($"Bank with name {bankName} successfully created");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error creating bank with name {bankName}");
            throw;
        }
    }

    public async Task<List<string>> GetAllBankNames()
    {
        try
        {
            _logger.Information("Attempting to retrieve all bank names");
            
            var bankNames = await _bankRepository.GetAllBankNamesAsync();
            _logger.Information("Successfully retrieved all bank names");
            return bankNames.ToList();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all bank names");
            throw;
        }
    }

    public async Task<Bank> GetBankByName(string name)
    {
        try
        {
            _logger.Information($"Attempting to retrieve bank with name: {name}");
            
            var bank = await _bankRepository.GetByNameAsync(name);
            if (bank == null)
            {
                _logger.Error($"Bank with name {name} not found");
                throw new ApplicationException($"Bank {name} not found");
            }
            _logger.Information($"Successfully retrieved bank with name {name}");
            return bank;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving bank with name {name}");
            throw;
        }
    }

    public async Task<IEnumerable<Bank>> GetAllBanks()
    {
        try
        {
            _logger.Information("Attempting to retrieve all banks");
            
            var banks = await _bankRepository.GetAllBanksAsync();
            _logger.Information("Successfully retrieved all banks");
            return banks;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all banks");
            throw;
        }
    }
}