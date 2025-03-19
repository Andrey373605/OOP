using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    IDataBaseHelper _dataBaseHelper;

    public BankRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }
    public async Task AddAsync(Bank bank)
    {
        string query = @"INSERT INTO Bank 
                         (Name) 
                         VALUES 
                         (@Name)";

        var parameters = new Dictionary<string, object>
        {
            {"Name", bank.Name}
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<Bank> GetByIdAsync(int id)
    {
        string query = @"SELECT Id, Name 
                         FROM Bank 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", id}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если банк не найден
        }

        var row = result[0];
        
        var bank = new Bank
        {
            Id = Convert.ToInt32(row["Id"]),
            Name = row["Name"].ToString()
        };

        return bank;
    }

    public async Task<Bank> GetByNameAsync(string name)
    {
        string query = @"SELECT Id, Name 
                         FROM Bank 
                         WHERE Name = @Name";

        var parameters = new Dictionary<string, object>
        {
            {"Name", name}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если банк не найден
        }

        var row = result[0];
        
        var bank = new Bank
        {
            Id = Convert.ToInt32(row["Id"]),
            Name = row["Name"].ToString()
        };

        return bank;
    }

    public async Task UpdateAsync(Bank bank)
    {
        string query = @"UPDATE Bank 
                         SET Name = @Name 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", bank.Id},
            {"Name", bank.Name}
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<IEnumerable<string>> GetAllBankNamesAsync()
    {
        string query = @"SELECT Name 
                         FROM Bank";

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query));

        var bankNames = new List<string>();

        foreach (var row in result)
        {
            bankNames.Add(row["Name"].ToString());
        }

        return bankNames;
    }

    public async Task<IEnumerable<Bank>> GetAllBanksAsync()
    {
        string query = @"SELECT Id, Name 
                         FROM Bank";

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query));

        var banks = new List<Bank>();

        foreach (var row in result)
        {
            banks.Add(new Bank
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()
            });
        }

        return banks;
    }
}