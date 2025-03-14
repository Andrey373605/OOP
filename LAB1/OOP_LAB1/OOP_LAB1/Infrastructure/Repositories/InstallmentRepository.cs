using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class InstallmentRepository : IInstallmentRepository
{
    private readonly IDataBaseHelper _dataBaseHelper;

    public InstallmentRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }
    public async Task AddAsync(Installment installment)
    {
        var query = @"
        INSERT INTO Installment (AccountId, ClientId, Amount, NumberOfPayments, RestMonth, IsActive)
        VALUES (@AccountId, @ClientId, @Amount, @NumberOfPayments, @RestMonth, @IsActive);
    ";

        var parameters = new Dictionary<string, object>
        {
            { "AccountId", installment.AccountId },
            { "ClientId", installment.ClientId },
            { "Amount", installment.Amount },
            { "NumberOfPayments", installment.NumberOfPayments },
            { "RestMonth", installment.RestMonth },
            { "IsActive", installment.IsActive }
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }
    

    public async Task UpdateAsync(Installment installment)
    {
        var query = @"
            UPDATE Installment
            SET AccountId = @AccountId,
                ClientId = @ClientId,
                Amount = @Amount,
                NumberOfPayments = @NumberOfPayments,
                RestMonth = @RestMonth,
                IsActive = @IsActive
            WHERE Id = @Id;
        ";

        var parameters = new Dictionary<string, object>
        {
            { "Id", installment.Id },
            { "AccountId", installment.AccountId },
            { "ClientId", installment.ClientId },
            { "Amount", installment.Amount },
            { "NumberOfPayments", installment.NumberOfPayments },
            { "RestMonth", installment.RestMonth },
            { "IsActive", installment.IsActive }
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public Task DeleteAsync(Installment installment)
    {
        throw new NotImplementedException();
    }

    public async Task<Installment> GetByIdAsync(int installmentId)
    {
        var query = @"
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, RestMonth, IsActive
                FROM Installment
                WHERE Id = @Id;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "Id", installmentId }
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null;
        }

        var row = result[0];
        return new Installment
        {
            Id = Convert.ToInt32(row["Id"]),
            AccountId = Convert.ToInt32(row["AccountId"]),
            ClientId = Convert.ToInt32(row["ClientId"]),
            Amount = Convert.ToDecimal(row["Amount"]),
            NumberOfPayments = Convert.ToInt32(row["NumberOfPayments"]),
            RestMonth = Convert.ToInt32(row["RestMonth"]),
            IsActive = Convert.ToBoolean(row["IsActive"])
        };
    }
}