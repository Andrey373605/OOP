using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger _logger;

    public ClientService(IClientRepository clientRepository, ILogger logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }
    
    public async Task<Client> GetClientByUserIdAsync(int bankId, int userId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve client with user ID: {userId} from bank ID: {bankId}");
            
            var client = await _clientRepository.GetClientByUserIdAsync(bankId, userId);
            if (client == null)
            {
                _logger.Error($"Client with user ID {userId} not found in bank ID {bankId}");
                throw new NullReferenceException($"Client with id {userId} not found");
            }
            _logger.Information($"Successfully retrieved client with user ID {userId} from bank ID {bankId}");
            return client;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving client with user ID {userId} from bank ID {bankId}");
            throw;
        }
    }

    public async Task<IEnumerable<Client>> GetClientRegistrationRequests()
    {
        try
        {
            _logger.Information("Attempting to retrieve client registration requests");
            
            var requests = await _clientRepository.GetClientRegistrationRequests();
            _logger.Information("Successfully retrieved client registration requests");
            return requests;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving client registration requests");
            throw;
        }
    }

    public async Task ApproveClientRegistration(int id)
    {
        try
        {
            _logger.Information($"Attempting to approve client registration with ID: {id}");
            
            var client = await _clientRepository.GetRequestByIdAsync(id);
            if (client == null)
            {
                _logger.Error($"Client with ID {id} not found");
                throw new NullReferenceException($"Client with id {id} not found");
            }
            client.Activate();
            await _clientRepository.UpdateAsync(client);
            _logger.Information($"Successfully approved client registration with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error approving client registration with ID {id}");
            throw;
        }
    }

    public async Task RejectClientRegistration(int id)
    {
        try
        {
            _logger.Information($"Attempting to reject client registration with ID: {id}");
            
            var client = await _clientRepository.GetRequestByIdAsync(id);
            if (client == null)
            {
                _logger.Error($"Client with ID {id} not found");
                throw new NullReferenceException($"Client with id {id} not found");
            }
            client.Reject();
            await _clientRepository.UpdateAsync(client);
            _logger.Information($"Successfully rejected client registration with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error rejecting client registration with ID {id}");
            throw;
        }
    }
}