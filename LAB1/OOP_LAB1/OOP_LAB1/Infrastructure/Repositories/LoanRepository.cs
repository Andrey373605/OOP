using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    IDataBaseHelper _dataBaseHelper;

    public LoanRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }
    public async Task AddAsync(Loan loan)
    {
        var query = @"
        INSERT INTO Loan (AccountId, ClientId, Amount, NumberOfPayments, InterestRate, RestMonth, IsActive)
        VALUES (@AccountId, @ClientId, @Amount, @NumberOfPayments, @InterestRate, @RestMonth, @IsActive);";

        var parameters = new Dictionary<string, object>
        {
            { "AccountId", loan.AccountId },
            { "ClientId", loan.ClientId },
            { "Amount", loan.Amount },
            { "NumberOfPayments", loan.NumberOfPayments },
            { "InterestRate", loan.InterestRate },
            { "RestMonth", loan.RestMonth },
            { "IsActive", loan.IsActive }
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task UpdateAsync(Loan loan)
    {
        var query = @"
            UPDATE Loan
            SET AccountId = @AccountId,
                ClientId = @ClientId,
                Amount = @Amount,
                NumberOfPayments = @NumberOfPayments,
                InterestRate = @InterestRate,
                RestMonth = @RestMonth,
                IsActive = @IsActive
            WHERE Id = @Id;
        ";

        var parameters = new Dictionary<string, object>
        {
            { "Id", loan.Id },
            { "AccountId", loan.AccountId },
            { "ClientId", loan.ClientId },
            { "Amount", loan.Amount },
            { "NumberOfPayments", loan.NumberOfPayments },
            { "InterestRate", loan.InterestRate },
            { "RestMonth", loan.RestMonth },
            { "IsActive", loan.IsActive }
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
        
    }

    public Task DeleteAsync(Loan loan)
    {
        throw new NotImplementedException();
    }

    public async Task<Loan> GetByIdAsync(int loanId)
    {
        var query = @"
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, InterestRate, RestMonth, IsActive
                FROM Loan
                WHERE Id = @Id;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "Id", loanId }
        };

        var result = await Task.Run(() =>_dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null;
        }

        var row = result[0];
        return new Loan
        {
            Id = Convert.ToInt32(row["Id"]),
            AccountId = Convert.ToInt32(row["AccountId"]),
            ClientId = Convert.ToInt32(row["ClientId"]),
            Amount = Convert.ToDecimal(row["Amount"]),
            NumberOfPayments = Convert.ToInt32(row["NumberOfPayments"]),
            InterestRate = Convert.ToInt32(row["InterestRate"]),
            RestMonth = Convert.ToDecimal(row["RestMonth"]),
            IsActive = Convert.ToBoolean(row["IsActive"])
        };
    }

    public async Task<IEnumerable<Loan>> GetAllByClientId(int clientId)
    {
        var query = @"
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, InterestRate, RestMonth, IsActive
                FROM Loan
                WHERE ClientId = @ClientId;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "ClientId", clientId }
        };

        var result = await Task.Run(() =>_dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null;
        }
        
        var loans = new List<Loan>();

        foreach (var row in result)
        {
            loans.Add(new Loan
            {
                Id = Convert.ToInt32(row["Id"]),
                AccountId = Convert.ToInt32(row["AccountId"]),
                ClientId = Convert.ToInt32(row["ClientId"]),
                Amount = Convert.ToDecimal(row["Amount"]),
                NumberOfPayments = Convert.ToInt32(row["NumberOfPayments"]),
                InterestRate = Convert.ToInt32(row["InterestRate"]),
                RestMonth = Convert.ToDecimal(row["RestMonth"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            });
        }

        return loans;
    }
}