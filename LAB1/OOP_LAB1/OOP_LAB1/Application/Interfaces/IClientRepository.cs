using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IClientRepository
{
    public Task UpdateAsync(Client client);

    public Task<Client> GetRequestByIdAsync(int id);
}