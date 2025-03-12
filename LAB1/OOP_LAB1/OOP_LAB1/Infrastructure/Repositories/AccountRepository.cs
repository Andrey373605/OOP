using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Infrastructure.Data;

using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private IDataBaseHelper _databaseHelper;

    public AccountRepository(IDataBaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    } 
    public async Task UpdateAsync(Account account)
    {
        string query = @"UPDATE Account 
                         SET BankId = @BankId, 
                             Balance = @Balance, 
                             ClientId = @ClientId, 
                             Status = @Status, 
                             AccountType = @AccountType 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", account.Id},
            {"BankId", account.BankId},
            {"Balance", account.Balance},
            {"ClientId", account.ClientId},
            {"Status", account.Status},
            {"AccountType", account.AccountType}
        };

        await Task.Run(() => _databaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task AddAsync(Account account)
    {
        string query = @"INSERT INTO Account 
                         (BankId, Balance, ClientId, Status, AccountType) 
                         VALUES 
                         (@BankId, @Balance, @ClientId, @Status, @AccountType)";

        var parameters = new Dictionary<string, object>
        {
            {"BankId", account.BankId},
            {"Balance", account.Balance},
            {"ClientId", account.ClientId},
            {"Status", account.Status},
            {"AccountType", account.AccountType}
        };

        await Task.Run(() => _databaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task RemoveAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        string query = @"SELECT Id, BankId, Balance, ClientId, Status, AccountType 
                         FROM Account 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", id}
        };

        var result = await Task.Run(() => _databaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null;
        }

        var row = result[0];
        
        var account = new Account
        {
            Id = Convert.ToInt32(row["Id"]),
            BankId = Convert.ToInt32(row["BankId"]),
            Balance = Convert.ToDecimal(row["Balance"]),
            ClientId = Convert.ToInt32(row["ClientId"]),
            Status = (AccountStatus)Convert.ToInt32(row["Status"]),
            AccountType = (AccountType)Convert.ToInt32(row["AccountType"])
        };

        return account;
    }

    public async Task<IEnumerable<Account>> GetAllByClientIdAsync(int clientId)
    {
        string query = @"SELECT Id, BankId, Balance, ClientId, Status, AccountType 
                         FROM Account 
                         WHERE ClientId = @ClientId";

        var parameters = new Dictionary<string, object>
        {
            {"ClientId", clientId}
        };

        var result = await Task.Run(() => _databaseHelper.ExecuteQuery(query, parameters));
        
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
}