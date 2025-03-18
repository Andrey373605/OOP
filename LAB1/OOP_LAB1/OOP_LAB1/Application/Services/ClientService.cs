using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class ClientService : IClientService
{
    IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task<Client> GetClientByUserIdAsync(int bankId, int userId)
    {
        var client = await _clientRepository.GetClientByUserIdAsync(bankId, userId);
        if (client == null)
        {
            throw new NullReferenceException($"Client with id {userId} not found");
        }
        return client;
    }
}