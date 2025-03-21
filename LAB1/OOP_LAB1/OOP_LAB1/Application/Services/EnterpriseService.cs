using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly ILogger _logger;

    public EnterpriseService(IEnterpriseRepository enterpriseRepository, ILogger logger)
    {
        _enterpriseRepository = enterpriseRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Enterprise>> GetAllEnterprises()
    {
        try
        {
            _logger.Information("Attempting to retrieve all enterprises");
            
            var enterprises = await _enterpriseRepository.GetAllEnterprisesAsync();
            _logger.Information("Successfully retrieved all enterprises");
            return enterprises;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving all enterprises");
            throw;
        }
    }
}