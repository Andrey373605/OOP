using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface ILoanService
{
    public Task DepositMoney(int loanId);

    public Task CreateLoanRequest(int clientId, decimal depositAmount, int interestRate, int monthCount);

    public Task ApproveLoanRequest(int loanId);
}