using System.Collections;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
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
        INSERT INTO Installment (AccountId, ClientId, Amount, NumberOfPayments, RestMonth, Status, StartDate)
        VALUES (@AccountId, @ClientId, @Amount, @NumberOfPayments, @RestMonth, @Status, @StartDate);
    ";

        var parameters = new Dictionary<string, object>
        {
            { "AccountId", installment.AccountId },
            { "ClientId", installment.ClientId },
            { "Amount", installment.Amount },
            { "NumberOfPayments", installment.NumberOfPayments },
            { "RestMonth", installment.RestMonth },
            { "Status", installment.Status },
            { "StartDate", installment.StartDate }
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
                Status = @Status,
                StartDate = @StartDate
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
            { "Status", installment.Status },
            { "StartDate", installment.StartDate }
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
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, RestMonth, Status, StartDate
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
            Status = (InstallmentStatus)Convert.ToInt32(row["Status"]),
            StartDate = Convert.ToDateTime(row["StartDate"])
        };
    }

    public async Task<IEnumerable<Installment>> GetAllByClientId(int clientId)
    {
        var query = @"
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, RestMonth, Status, StartDate
                FROM Installment
                WHERE ClientId = @ClientId;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "ClientId", clientId }
        };

        var result = await Task.Run(() =>_dataBaseHelper.ExecuteQuery(query, parameters));
        
        
        var installments = new List<Installment>();

        foreach (var row in result)
        {
            installments.Add(new Installment
            {
                Id = Convert.ToInt32(row["Id"]),
                AccountId = Convert.ToInt32(row["AccountId"]),
                ClientId = Convert.ToInt32(row["ClientId"]),
                Amount = Convert.ToDecimal(row["Amount"]),
                NumberOfPayments = Convert.ToInt32(row["NumberOfPayments"]),
                RestMonth = Convert.ToInt32(row["RestMonth"]),
                Status = (InstallmentStatus)Convert.ToInt32(row["Status"]),
                StartDate = Convert.ToDateTime(row["StartDate"])
            });
        }

        return installments;
    }

    public async Task<IEnumerable<Installment>> GetInstallmentApplications()
    {
        var query = @"
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, RestMonth, Status
                FROM Installment
                WHERE Status = @Status;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "Status", InstallmentStatus.Application }
        };

        var result = await Task.Run(() =>_dataBaseHelper.ExecuteQuery(query, parameters));

        
        var installments = new List<Installment>();

        foreach (var row in result)
        {
            installments.Add(new Installment
            {
                Id = Convert.ToInt32(row["Id"]),
                AccountId = Convert.ToInt32(row["AccountId"]),
                ClientId = Convert.ToInt32(row["ClientId"]),
                Amount = Convert.ToDecimal(row["Amount"]),
                NumberOfPayments = Convert.ToInt32(row["NumberOfPayments"]),
                RestMonth = Convert.ToInt32(row["RestMonth"]),
                Status = (InstallmentStatus)Convert.ToInt32(row["Status"]),
                StartDate = Convert.ToDateTime(row["StartDate"])
            });
        }

        return installments;
    }

    public async Task<IEnumerable<Installment>> GetAllActiveInstallments()
    {
        var query = @"
                SELECT Id, AccountId, ClientId, Amount, NumberOfPayments, RestMonth, Status
                FROM Installment
                WHERE Status = @Status;
            ";

        var parameters = new Dictionary<string, object>
        {
            { "Status", InstallmentStatus.Active }
        };

        var result = await Task.Run(() =>_dataBaseHelper.ExecuteQuery(query, parameters));

        
        var installments = new List<Installment>();

        foreach (var row in result)
        {
            installments.Add(new Installment
            {
                Id = Convert.ToInt32(row["Id"]),
                AccountId = Convert.ToInt32(row["AccountId"]),
                ClientId = Convert.ToInt32(row["ClientId"]),
                Amount = Convert.ToDecimal(row["Amount"]),
                NumberOfPayments = Convert.ToInt32(row["NumberOfPayments"]),
                RestMonth = Convert.ToInt32(row["RestMonth"]),
                Status = (InstallmentStatus)Convert.ToInt32(row["Status"]),
                StartDate = Convert.ToDateTime(row["StartDate"])
            });
        }

        return installments;
    }
}