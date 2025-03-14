using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IClientService
{
    public Task<Client> GetClientByUserIdAsync(int bankId, int userId);
}