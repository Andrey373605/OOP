﻿using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    public Task UpdateAsync(Client client)
    {
        throw new NotImplementedException();
    }

    public Task<Client> GetRequestByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Client client)
    {
        throw new NotImplementedException();
    }
}