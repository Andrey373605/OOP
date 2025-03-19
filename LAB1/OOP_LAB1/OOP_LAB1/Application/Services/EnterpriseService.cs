using System.Collections;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class EnterpriseService : IEnterpriseService
{
    private IEnterpriseRepository _enterpriseRepository;

    public EnterpriseService(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<IEnumerable<Enterprise>> GetAllEnterprises()
    {
        return await _enterpriseRepository.GetAllEnterprisesAsync();
    }
}