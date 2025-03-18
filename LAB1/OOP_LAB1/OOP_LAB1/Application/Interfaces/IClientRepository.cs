using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IClientRepository
{
    public Task UpdateAsync(Client client);
    public Task<Client> GetRequestByIdAsync(int id);
    public Task AddAsync(Client client);
    public Task<Client> GetByIdAsync(int clientId);
    public Task<IEnumerable<Account>> GetAllAccountsByClientIdAsync(int clientId);

    public Task<Client> GetClientByUserIdAsync(int bankId, int userId);
    Task<IEnumerable<Client>> GetClientRegistrationRequests();
}