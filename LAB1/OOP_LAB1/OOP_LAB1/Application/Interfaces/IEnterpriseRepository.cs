using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IEnterpriseRepository
{
    Task<Enterprise> GetByIdAsync(int enterpriseid);
}