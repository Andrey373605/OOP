﻿using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    
    IDataBaseHelper _dataBaseHelper;

    public ClientRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }
    
    public async Task UpdateAsync(Client client)
    {
        string query = @"UPDATE Client 
                         SET UserId = @UserId, 
                             BankId = @BankId, 
                             FirstName = @FirstName, 
                             LastName = @LastName, 
                             MiddleName = @MiddleName, 
                             PassportSeries = @PassportSeries, 
                             IdentificationNumber = @IdentificationNumber, 
                             Phone = @Phone, 
                             Status = @Status 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", client.Id},
            {"UserId", client.UserId},
            {"BankId", client.BankId},
            {"FirstName", client.FirstName},
            {"LastName", client.LastName},
            {"MiddleName", client.MiddleName},
            {"PassportSeries", client.PassportSeries},
            {"IdentificationNumber", client.IdentificationNumber},
            {"Phone", client.Phone},
            {"Status", client.Status}
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<Client> GetRequestByIdAsync(int id)
    {
        string query = @"SELECT Id, UserId, BankId, FirstName, LastName, MiddleName, 
                         PassportSeries, IdentificationNumber, Phone, Status 
                         FROM Client 
                         WHERE Id = @Id AND Status = @Status";

        var parameters = new Dictionary<string, object>
        {
            {"Id", id},
            {"Status", ClientStatus.Application}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если клиент не найден
        }

        var row = result[0];

        var client = new Client
        {
            Id = Convert.ToInt32(row["Id"]),
            UserId = Convert.ToInt32(row["UserId"]),
            BankId = Convert.ToInt32(row["BankId"]),
            FirstName = row["FirstName"].ToString(),
            LastName = row["LastName"].ToString(),
            MiddleName = row["MiddleName"].ToString(),
            PassportSeries = row["PassportSeries"].ToString(),
            IdentificationNumber = row["IdentificationNumber"].ToString(),
            Phone = row["Phone"].ToString(),
            Status = (ClientStatus)Convert.ToInt32(row["Status"])
        };

        return client;
    }

    public async Task AddAsync(Client client)
    {
        string query = @"INSERT INTO Client 
                         (UserId, BankId, FirstName, LastName, MiddleName, 
                         PassportSeries, IdentificationNumber, Phone, Status) 
                         VALUES 
                         (@UserId, @BankId, @FirstName, @LastName, @MiddleName, 
                         @PassportSeries, @IdentificationNumber, @Phone, @Status)";

        var parameters = new Dictionary<string, object>
        {
            {"UserId", client.UserId},
            {"BankId", client.BankId},
            {"FirstName", client.FirstName},
            {"LastName", client.LastName},
            {"MiddleName", client.MiddleName},
            {"PassportSeries", client.PassportSeries},
            {"IdentificationNumber", client.IdentificationNumber},
            {"Phone", client.Phone},
            {"Status", client.Status }
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<Client> GetByIdAsync(int clientId)
    {
        string query = @"SELECT Id, UserId, BankId, FirstName, LastName, MiddleName, 
                         PassportSeries, IdentificationNumber, Phone, Status 
                         FROM Client 
                         WHERE Id = @Id AND Status = @Status";

        var parameters = new Dictionary<string, object>
        {
            {"Id", clientId},
            {"Status", ClientStatus.Active}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если клиент не найден
        }

        var row = result[0];

        var client = new Client
        {
            Id = Convert.ToInt32(row["Id"]),
            UserId = Convert.ToInt32(row["UserId"]),
            BankId = Convert.ToInt32(row["BankId"]),
            FirstName = row["FirstName"].ToString(),
            LastName = row["LastName"].ToString(),
            MiddleName = row["MiddleName"].ToString(),
            PassportSeries = row["PassportSeries"].ToString(),
            IdentificationNumber = row["IdentificationNumber"].ToString(),
            Phone = row["Phone"].ToString(),
            Status = (ClientStatus)Convert.ToInt32(row["Status"])
        };

        return client;
    }

    public async Task<IEnumerable<Account>> GetAllAccountsByClientIdAsync(int clientId)
    {
        string query = @"SELECT Id, BankId, Balance, ClientId, Status, AccountType 
                         FROM Account 
                         WHERE ClientId = @ClientId";

        var parameters = new Dictionary<string, object>
        {
            {"ClientId", clientId}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));
        
        var accounts = new List<Account>();

        foreach (var row in result)
        {
            var account = new Account
            {
                Id = Convert.ToInt32(row["Id"]),
                BankId = Convert.ToInt32(row["BankId"]),
                Balance = Convert.ToDecimal(row["Balance"]),
                ClientId = Convert.ToInt32(row["ClientId"]),
                Status = (AccountStatus)Convert.ToInt32(row["Status"]),
                AccountType = (AccountType)Convert.ToInt32(row["AccountType"])
            };

            accounts.Add(account);
        }

        return accounts;
    }
    
    public async Task<Client> GetClientByUserIdAsync(int bankId, int userId)
    {
        string query = @"SELECT Id, UserId, BankId, FirstName, LastName, MiddleName, 
                         PassportSeries, IdentificationNumber, Phone, Status 
                         FROM Client 
                         WHERE BankId = @BankId AND UserId = @UserId";

        var parameters = new Dictionary<string, object>
        {
            {"BankId", bankId},
            {"UserId", userId}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если клиент не найден
        }

        var row = result[0];

        var client = new Client
        {
            Id = Convert.ToInt32(row["Id"]),
            UserId = Convert.ToInt32(row["UserId"]),
            BankId = Convert.ToInt32(row["BankId"]),
            FirstName = row["FirstName"].ToString(),
            LastName = row["LastName"].ToString(),
            MiddleName = row["MiddleName"].ToString(),
            PassportSeries = row["PassportSeries"].ToString(),
            IdentificationNumber = row["IdentificationNumber"].ToString(),
            Phone = row["Phone"].ToString(),
            Status = (ClientStatus)Convert.ToInt32(row["Status"])
        };

        return client;
    }

    public async Task<IEnumerable<Client>> GetClientRegistrationRequests()
    {
        string query = @"SELECT Id, UserId, BankId, FirstName, LastName, MiddleName, 
                         PassportSeries, IdentificationNumber, Phone, Status 
                         FROM Client 
                         WHERE Status = @Status";

        var parameters = new Dictionary<string, object>
        {
            {"Status", ClientStatus.Application }
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        var requests = new List<Client>();

        foreach (var row in result)
        {
            var client = new Client
            {
                Id = Convert.ToInt32(row["Id"]),
                UserId = Convert.ToInt32(row["UserId"]),
                BankId = Convert.ToInt32(row["BankId"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                PassportSeries = row["PassportSeries"].ToString(),
                IdentificationNumber = row["IdentificationNumber"].ToString(),
                Phone = row["Phone"].ToString(),
                Status = (ClientStatus)Convert.ToInt32(row["Status"])
            };
            requests.Add(client);
        }
        
        return requests;
    }
}