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

    public Task<IEnumerable<Client>> GetClientRegistrationRequests()
    {
        var requests = _clientRepository.GetClientRegistrationRequests();
        return requests;
    }

    public async Task ApproveClientRegistration(int id)
    {
        var client = await _clientRepository.GetRequestByIdAsync(id);
        if (client == null)
        {
            throw new NullReferenceException($"Client with id {id} not found");
        }
        client.Activate();
        await _clientRepository.UpdateAsync(client);
    }

    public async Task RejectClientRegistration(int id)
    {
        var client = await _clientRepository.GetRequestByIdAsync(id);
        if (client == null)
        {
            throw new NullReferenceException($"Client with id {id} not found");
        }
        client.Reject();
        await _clientRepository.UpdateAsync(client);
    }
}