using System.Collections;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IEnterpriseService
{
    Task<IEnumerable<Enterprise>> GetAllEnterprises();
}