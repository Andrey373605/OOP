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
        VALUES (@AccountId, @ClientId, @Amount, @NumberOfPayments, @InterestRate, @RestMonth, @IsActive);
    ";

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

    public Task UpdateAsync(Loan loan)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Loan loan)
    {
        throw new NotImplementedException();
    }

    public Task<Loan> GetByIdAsync(int loanId)
    {
        throw new NotImplementedException();
    }
}