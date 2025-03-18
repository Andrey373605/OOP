using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    IDataBaseHelper _dataBaseHelper;

    public TransactionRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }

    public async Task<Transaction> GetByIdAsync(int transactionId)
    {
        var query = @"
                SELECT Id, FromAccountId, ToAccountId, Amount, Date, Type
                FROM [Transaction]
                WHERE Id = @Id;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "Id", transactionId }
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null;
        }

        var row = result[0];
        return new Transaction
        {
            Id = Convert.ToInt32(row["Id"]),
            FromAccountId = Convert.ToInt32(row["FromAccountId"]),
            ToAccountId = Convert.ToInt32(row["ToAccountId"]),
            Amount = Convert.ToDecimal(row["Amount"]),
            Date = Convert.ToDateTime(row["Date"]),
            Type = (TransactionType)Convert.ToInt32(row["Type"])
        };
    }

    public async Task AddAsync(Transaction transaction)
    {
        var query = @"
        INSERT INTO [Transaction] (FromAccountId, ToAccountId, Amount, Date, Type)
        VALUES (@FromAccountId, @ToAccountId, @Amount, @Date, @Type);";

        var parameters = new Dictionary<string, object>
        {
            { "FromAccountId", transaction.FromAccountId },
            { "ToAccountId", transaction.ToAccountId },
            { "Amount", transaction.Amount },
            { "Date", transaction.Date },
            { "Type", transaction.Type },
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public Task UpdateAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Transaction>> GetTransferByAccountIdAsync(int accountId)
    {
        var query = @"
                SELECT Id, FromAccountId, ToAccountId, Amount, Date, Type
                FROM [Transaction]
                WHERE FromAccountId = @accountId OR ToAccountId = @accountId and Type = @Type;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "accountId", accountId },
            { "Type", TransactionType.Transfer }
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));


        var transactions = new List<Transaction>();

        foreach (var row in result)
        {
            transactions.Add(
                new Transaction
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FromAccountId = Convert.ToInt32(row["FromAccountId"]),
                    ToAccountId = Convert.ToInt32(row["ToAccountId"]),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    Date = Convert.ToDateTime(row["Date"]),
                    Type = (TransactionType)Convert.ToInt32(row["Type"])
                }
            );
        }
        return transactions;

    }

    public async Task<IEnumerable<Transaction>> GetDepositByAccountIdAsync(int accountId)
    {
        var query = @"
                SELECT Id, FromAccountId, ToAccountId, Amount, Date, Type
                FROM [Transaction]
                WHERE ToAccountId = @accountId and Type = @Type;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "accountId", accountId },
            { "Type", TransactionType.Deposit }
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));


        var transactions = new List<Transaction>();

        foreach (var row in result)
        {
            transactions.Add(
                new Transaction
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FromAccountId = Convert.ToInt32(row["FromAccountId"]),
                    ToAccountId = Convert.ToInt32(row["ToAccountId"]),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    Date = Convert.ToDateTime(row["Date"]),
                    Type = (TransactionType)Convert.ToInt32(row["Type"])
                }
            );
        }
        return transactions;
    }
    
    public async Task<IEnumerable<Transaction>> GetWithdrawByAccountIdAsync(int accountId)
    {
        var query = @"
                SELECT Id, FromAccountId, ToAccountId, Amount, Date, Type
                FROM [Transaction]
                WHERE FromAccountId = @accountId and Type = @Type;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "accountId", accountId },
            { "Type", TransactionType.Withdraw }
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));


        var transactions = new List<Transaction>();

        foreach (var row in result)
        {
            transactions.Add(
                new Transaction
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FromAccountId = Convert.ToInt32(row["FromAccountId"]),
                    ToAccountId = Convert.ToInt32(row["ToAccountId"]),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    Date = Convert.ToDateTime(row["Date"]),
                    Type = (TransactionType)Convert.ToInt32(row["Type"])
                }
            );
        }
        return transactions;
    }
}